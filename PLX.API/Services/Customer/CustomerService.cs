using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PLX.API.Constants;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;
using PLX.API.Data.DTO.LinkedCard;
using PLX.API.Data.DTO.Vehicle;
using PLX.API.Data.Repositories;
using PLX.API.Helpers;
using PLX.Persistence.Model;
using PLX.Persistence.Repository;

namespace PLX.API.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILinkedCardRepository _linkedCardRepository;
        private readonly ICustomerQuestionRepository _customerQuestionsRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IQuestionRepository _questionsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper,
            ICustomerRepository customerRepository, IVehicleRepository vehicleRepository,
            ILinkedCardRepository linkedCardRepository, ICustomerQuestionRepository customerQuestionsRepository,
            IQuestionRepository questionsRepository, IProvinceRepository provinceRepository,
            IDistrictRepository districtRepository, IWardRepository wardRepository, IResultMessageService iResultMessageService)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _linkedCardRepository = linkedCardRepository;
            _customerQuestionsRepository = customerQuestionsRepository;
            _questionsRepository = questionsRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
        }

        public async Task<APIResponse> RegisterAsync(CustomerRegister customerRegister)
        {
            var error = validateCustomer(customerRegister.CustomerInfo);
            if (error != null) return error;
            var exist = await _customerRepository.FindByPhone(customerRegister.CustomerInfo.CustomerBasic.Phone);
            if (exist != null)
                return ErrorResponse(ResultCodeConstants.RegisterError);
            var customer = _mapper.Map<CustomerRegister, Customer>(customerRegister);
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            return OkResponse(_mapper.Map<Customer, CustomerRegisterResponse>(customer), ResultCodeConstants.RegisterSuccess);
        }

        public async Task<APIResponse> GetStaticLists()
        {
            var question1 = await _questionsRepository.ListQuestionOne();
            var question2 = await _questionsRepository.ListQuestionTwo();
            var provinces = await _provinceRepository.ListAsync();
            // var questionList = _mapper.Map<List<Question>, List<ListItem>>(questions);
            var questionList1 = _mapper.Map<List<Question>, List<ListItem>>(question1);
            var questionList2 = _mapper.Map<List<Question>, List<ListItem>>(question2);
            var provinceList = _mapper.Map<List<Province>, List<ListItem>>(provinces);
            var genderList = new List<ListItem>();
            genderList.Add(new ListItem("male", "Nam"));
            genderList.Add(new ListItem("female", "Nữ"));
            genderList.Add(new ListItem("other", "Khác"));
            var customerStaticList = new CustomerStaticList
            {
                QuestionsOne = questionList1,
                QuestionsTwo = questionList2,
                Provinces = provinceList,
                Genders = genderList
            };
            return new ApiOkResponse(customerStaticList, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetDistrictsByProvinceId(int provinceId)
        {
            var districts = await _districtRepository.FindByProvinceId(provinceId);
            var result = new DistrictDTO(_mapper.Map<List<District>, List<ListItem>>(districts));
            return new ApiOkResponse(result, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetWardsByDistrictId(int districtId)
        {
            var wards = await _wardRepository.FindByDistrictId(districtId);
            var result = new WardDTO(_mapper.Map<List<Ward>, List<ListItem>>(wards));
            return new ApiOkResponse(result, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetCustomerById(BaseRequest baseRequest, int id)
        {
            var customer = await _customerRepository.FindById(id);
            if (customer == null)
                return ErrorResponse(ResultCodeConstants.ValidationExist);
            var customerResponse = _mapper.Map<Customer, GetCustomerResponse>(customer);
            return new ApiOkResponse(customerResponse, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> UpdateCustomer(int id, CustomerUpdateRequest customerUpdateRequest)
        {
            var customer = await _customerRepository.FindById(id);
            //kiem tra ton tai customer
            if (customer == null)
                return ErrorResponse(ResultCodeConstants.ValidationExist);
            if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.Name))
            {
                customer.Name = customerUpdateRequest.Customer.Name;
            }

            if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.Phone))
            {
                customer.Phone = customerUpdateRequest.Customer.Phone;
            }

            if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.CardId))
            {
                customer.CardID = customerUpdateRequest.Customer.CardId;
            }

            if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.Date))
            {
                customer.Date = DateTimeConvert.ToDate(customerUpdateRequest.Customer.Date);
            }

            if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.Gender))
            {
                customer.Gender = customerUpdateRequest.Customer.Gender;
            }
            if (!Validation.IsEqualOrLessThanZero(customerUpdateRequest.Customer.ProvinceId))
            {
                customer.ProvinceId = customerUpdateRequest.Customer.ProvinceId;
            }

            if (!Validation.IsEqualOrLessThanZero(customerUpdateRequest.Customer.DistrictId))
            {
                customer.DistrictId = customerUpdateRequest.Customer.DistrictId;
            }

            if (!Validation.IsEqualOrLessThanZero(customerUpdateRequest.Customer.WardId))
            {
                customer.WardId = customerUpdateRequest.Customer.WardId;
            }

            if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.Address))
            {
                customer.Address = customerUpdateRequest.Customer.Address;
            }
            var savedQuestions = customer.Questions.ToDictionary(x => x.QuestionId, x => x);

            foreach (var question in customerUpdateRequest.Questions)
            {
                CustomerQuestion savedQuestion = null;
                var exist = savedQuestions.TryGetValue(question.Id, out savedQuestion);

                if (!exist && question.RecordType == RecordTypes.NewRecord)
                {
                    var newQuestion = _mapper.Map<QuestionRequest, CustomerQuestion>(question);
                    await _customerQuestionsRepository.AddAsync(newQuestion);
                }

                if (exist && question.RecordType == RecordTypes.ExistRecord && !Validation.IsNullOrEmpty(question.Answer))
                {
                    savedQuestion.Answer = question.Answer;
                }
            }

            var savedVehicles = customer.Vehicles.ToDictionary(x => x.Id, x => x);
            foreach (var vehicle in customerUpdateRequest.Vehicles)
            {
                Vehicle savedVehicle = null;
                var exist = savedVehicles.TryGetValue(vehicle.Id, out savedVehicle);

                if (!exist && vehicle.RecordType == RecordTypes.NewRecord)
                {
                    var newVehicle = _mapper.Map<VehicleRequest, Vehicle>(vehicle);
                    await _vehicleRepository.AddAsync(newVehicle);
                }

                if (exist && vehicle.RecordType == RecordTypes.ExistRecord && !Validation.IsNullOrEmpty(vehicle.Name) && !Validation.IsNullOrEmpty(vehicle.LicensePlate) && !Validation.IsEqualOrLessThanZero(vehicle.VehicleTypeId))
                {
                    savedVehicle.Name = vehicle.Name;
                    savedVehicle.LicensePlate = vehicle.LicensePlate;
                    savedVehicle.VehicleTypeId = vehicle.VehicleTypeId;
                }
            }
            var savedLinkedCards = customer.LinkedCards.ToDictionary(x => x.Id, x => x);
            foreach (var linkedCard in customerUpdateRequest.LinkedCards)
            {
                Vehicle savedLinkedCard = null;
                var exist = savedVehicles.TryGetValue(linkedCard.Id, out savedLinkedCard);

                if (!exist && linkedCard.RecordType == RecordTypes.NewRecord)
                {
                    var newLinkedCard = _mapper.Map<LinkedCardRequest, LinkedCard>(linkedCard);
                    await _linkedCardRepository.AddAsync(newLinkedCard);
                }

                if (exist && linkedCard.RecordType == RecordTypes.ExistRecord && !Validation.IsNullOrEmpty(linkedCard.Name) && !Validation.IsNullOrEmpty(linkedCard.CardNumber))
                {
                    savedLinkedCard.Name = linkedCard.Name;
                    savedLinkedCard.LicensePlate = linkedCard.CardNumber;
                }
            }

            await this._unitOfWork.CompleteAsync();
            var customerUpdateResponse = _mapper.Map<Customer, CustomerUpdateResponse>(customer);
            return OkResponse(customerUpdateResponse, ResultCodeConstants.UpdateSuccess);
        }

        private ApiErrorResponse validateCustomer(CustomerInfo customerInfo)
        {
            var customerBasic = customerInfo.CustomerBasic;
            var isBizCustomer = customerBasic.CustomerTypeId == CustomerTypes.BizCustomer;

            if (Validation.IsNullOrEmpty(customerBasic.Name))
            {
                var argurments = new object[] { isBizCustomer ? "Tên đơn vị" : "Tên" };
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, argurments);
            }

            if (Validation.IsNullOrEmpty(customerBasic.Phone))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Số điện thoại" });
            }

            if (!Validation.IsValidPhone(customerBasic.Phone))
            {
                return ErrorResponse(ResultCodeConstants.EInvalidPhoneFormat);
            }

            if (Validation.IsNullOrEmpty(customerBasic.Password))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Mật khẩu" });
            }

            var customerCard = customerInfo.CustomerCard;
            if (Validation.IsNullOrEmpty(customerCard.Date))
            {
                var argurments = new object[] { isBizCustomer ? "Ngày thành lập" : "Ngày sinh" };
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, argurments);
            }

            var isValidDate = Validation.IsValidDate(customerCard.Date);
            if (!isValidDate)
            {
                var argurments = new object[] { isBizCustomer ? "Ngày thành lập" : "Ngày sinh" };
                return ErrorResponse(ResultCodeConstants.EInvalidDateFormat, argurments);
            }

            foreach (var item in customerBasic.Questions)
            {
                if (Validation.IsEqualOrLessThanZero(item.QuestionId))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Câu hỏi bí mật" });
                }
            }

            if (Validation.IsEqualOrLessThanZero(customerCard.ProvinceId))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Tỉnh" });
            }

            if (Validation.IsEqualOrLessThanZero(customerCard.DistrictId))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Quận/Huyện" });
            }

            if (Validation.IsEqualOrLessThanZero(customerCard.WardId))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Phường/Xã" });
            }

            if (Validation.IsNullOrEmpty(customerCard.Address))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Địa chỉ liên hệ" });
            }

            if (isBizCustomer)
            {
                if (Validation.IsNullOrEmpty(customerBasic.Email))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Email" });
                }
                if (Validation.IsNullOrEmpty(customerCard.TaxCode))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Mã số thuế" });
                }
            }
            else
            {
                if (Validation.IsNullOrEmpty(customerCard.CardId))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Số CMND/CCCD" });
                }

                if (Validation.IsNullOrEmpty(customerCard.Gender))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Giới tính" });
                }
            }

            return null;
        }
    }
}
