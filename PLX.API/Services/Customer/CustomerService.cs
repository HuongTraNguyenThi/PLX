using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using PLX.API.Constants;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Authentication;
using PLX.API.Data.DTO.Customer;
using PLX.API.Data.DTO.LinkedCard;
using PLX.API.Data.DTO.Vehicle;
using PLX.API.Helpers;
using PLX.Persistence.Model;
using PLX.Persistence.Repository;
using BC = BCrypt.Net.BCrypt;

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
        private JwtConfig _jwtConfig;
        private IMapper _mapper;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper,
            ICustomerRepository customerRepository, IVehicleRepository vehicleRepository,
            ILinkedCardRepository linkedCardRepository, ICustomerQuestionRepository customerQuestionsRepository,
            IQuestionRepository questionsRepository, IProvinceRepository provinceRepository,
            IDistrictRepository districtRepository, IWardRepository wardRepository, IOptions<JwtConfig> options,
            IResultMessageService iResultMessageService)
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
            _jwtConfig = options.Value;
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
            return OkResponse(customerStaticList, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetDistrictsByProvinceId(int provinceId)
        {
            var districts = await _districtRepository.FindByProvinceId(provinceId);
            if (districts.Count == 0)
                return ErrorResponse(ResultCodeConstants.NotFound);
            var result = new DistrictDTO(_mapper.Map<List<District>, List<ListItem>>(districts));
            return OkResponse(result, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetWardsByDistrictId(int districtId)
        {
            var wards = await _wardRepository.FindByDistrictId(districtId);
            if (wards.Count == 0)
                return ErrorResponse(ResultCodeConstants.NotFound);
            var result = new WardDTO(_mapper.Map<List<Ward>, List<ListItem>>(wards));
            return OkResponse(result, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetCustomerById(BaseRequest baseRequest, int id)
        {

            var customer = await _customerRepository.FindById(id);
            if (customer == null)
                return ErrorResponse(ResultCodeConstants.ValidationExist);
            var customerResponse = _mapper.Map<Customer, GetCustomerResponse>(customer);

            return OkResponse(customerResponse, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> UpdateCustomer(int id, CustomerUpdateRequest customerUpdateRequest)
        {
            var customer = await _customerRepository.FindById(id);
            //kiem tra ton tai customer
            if (customer == null)
                return ErrorResponse(ResultCodeConstants.ValidationExist);
            if (customer.CustomerTypeId == CustomerTypes.IndividualCustomer)
            {
                if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.Name))
                {
                    customer.Name = customerUpdateRequest.Customer.Name;
                }
                if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.Date))
                {
                    customer.Date = DateTimeConvert.ToDate(customerUpdateRequest.Customer.Date);
                }

                if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.Gender))
                {
                    customer.Gender = customerUpdateRequest.Customer.Gender;
                }

                if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.TaxCode))
                {
                    customer.TaxCode = customerUpdateRequest.Customer.TaxCode;
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
            }
            if (customer.CustomerTypeId == CustomerTypes.BizCustomer)
            {
                if (!Validation.IsNullOrEmpty(customerUpdateRequest.Customer.Name))
                {
                    customer.Name = customerUpdateRequest.Customer.Name;
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
            }
            var savedQuestions = customer.Questions.ToDictionary(x => x.QuestionId, x => x);

            foreach (var question in customerUpdateRequest.Questions)
            {
                CustomerQuestion savedQuestion = null;
                var exist = savedQuestions.TryGetValue(question.Id, out savedQuestion);

                if (!exist && question.RecordType == RecordTypes.NewRecord)
                {

                    var newQuestion = _mapper.Map<QuestionRequest, CustomerQuestion>(question);
                    newQuestion.CustomerId = customer.Id;

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
                var checkNullOrEmpty = Validation.CheckNullOrEmptyVehicle(vehicle);

                if (!exist && vehicle.RecordType == RecordTypes.NewRecord)
                {
                    var newVehicle = _mapper.Map<VehicleRequest, Vehicle>(vehicle);
                    newVehicle.CustomerId = customer.Id;
                    await _vehicleRepository.AddAsync(newVehicle);
                }

                if (exist && vehicle.RecordType == RecordTypes.ExistRecord && !checkNullOrEmpty)
                {
                    savedVehicle.Name = vehicle.Name;
                    savedVehicle.LicensePlate = vehicle.LicensePlate;
                    savedVehicle.VehicleTypeId = vehicle.VehicleTypeId;
                }
                if (exist && vehicle.RecordType == RecordTypes.DeleteRecord)
                {
                    _vehicleRepository.Remove(savedVehicle);
                }


            }
            var savedLinkedCards = customer.LinkedCards.ToDictionary(x => x.Id, x => x);
            foreach (var linkedCard in customerUpdateRequest.LinkedCards)
            {
                LinkedCard savedLinkedCard = null;
                var checkNullOrEmpty = Validation.CheckNullOrEmptyLinkedCard(linkedCard);
                var exist = savedLinkedCards.TryGetValue(linkedCard.Id, out savedLinkedCard);

                if (!exist && linkedCard.RecordType == RecordTypes.NewRecord)
                {
                    var newLinkedCard = _mapper.Map<LinkedCardRequest, LinkedCard>(linkedCard);
                    newLinkedCard.CustomerId = customer.Id;
                    await _linkedCardRepository.AddAsync(newLinkedCard);
                }

                if (exist && linkedCard.RecordType == RecordTypes.ExistRecord && !checkNullOrEmpty)
                {
                    savedLinkedCard.Name = linkedCard.Name;
                    savedLinkedCard.CardNumber = linkedCard.CardNumber;
                }
                if (exist && linkedCard.RecordType == RecordTypes.DeleteRecord)
                {
                    _linkedCardRepository.Remove(savedLinkedCard);
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
                var argurments = new object[] { isBizCustomer ? Messages.BizName: Messages.IndividualName};
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, argurments);
            }

            if (Validation.IsNullOrEmpty(customerBasic.Phone))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] {Messages.Phone });
            }

            if (!Validation.IsValidPhone(customerBasic.Phone))
            {
                return ErrorResponse(ResultCodeConstants.EInvalidPhoneFormat);
            }

            if (Validation.IsNullOrEmpty(customerBasic.Password))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.Password} );
            }

            var customerCard = customerInfo.CustomerCard;
            if (Validation.IsNullOrEmpty(customerCard.Date))
            {
                var argurments = new object[] { isBizCustomer ? Messages.BizDate : Messages.IndividualDate };
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, argurments);
            }

            var isValidDate = Validation.IsValidDate(customerCard.Date);
            if (!isValidDate)
            {
                var argurments = new object[] { isBizCustomer ? Messages.BizDate : Messages.IndividualDate};
                return ErrorResponse(ResultCodeConstants.EInvalidDateFormat, argurments);
            }

            foreach (var item in customerBasic.Questions)
            {
                if (Validation.IsEqualOrLessThanZero(item.QuestionId))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.Questions });
                }
            }

            if (Validation.IsEqualOrLessThanZero(customerCard.ProvinceId))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.Province  });
            }

            if (Validation.IsEqualOrLessThanZero(customerCard.DistrictId))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.District });
            }

            if (Validation.IsEqualOrLessThanZero(customerCard.WardId))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.Ward });
            }

            if (Validation.IsNullOrEmpty(customerCard.Address))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.Address });
            }

            if (isBizCustomer)
            {
                if (Validation.IsNullOrEmpty(customerBasic.Email))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.Email });
                }
                if (Validation.IsNullOrEmpty(customerCard.TaxCode))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.TaxCode});
                }
            }
            else
            {
                if (Validation.IsNullOrEmpty(customerCard.CardId))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.CardId });
                }

                if (Validation.IsNullOrEmpty(customerCard.Gender))
                {
                    return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.Gender });
                }
            }

            return null;
        }

        public async Task<APIResponse> GetCustomerQuestions(GetQuestionsRequest questionsRequest)
        {
            var customerQuestions = await _customerQuestionsRepository.FindByPhone(questionsRequest.Phone);
            if (customerQuestions.Count == 0)
                return ErrorResponse(ResultCodeConstants.NotFound);
            var questions = customerQuestions.Select(x => x.Question).ToList();
            var result = new GetQuestionResponse(_mapper.Map<List<Question>, List<ListItem>>(questions));
            return OkResponse(result, ResultCodeConstants.Success);
        }
        public async Task<APIResponse> ChangePassword(int id, ChangePasswordRequest changePasswordRequest)
        {
            var customer = await _customerRepository.FindById(id);
            //kiem tra ton tai customer
            if (customer == null)
                return ErrorResponse(ResultCodeConstants.ValidationExist);
            if (!Validation.IsNullOrEmpty(changePasswordRequest.NewPassword) && !Validation.IsNullOrEmpty(changePasswordRequest.ConfirmNewPassword))
            {

                if (changePasswordRequest.ConfirmNewPassword == changePasswordRequest.NewPassword)
                {
                    customer.Password = BC.HashPassword(changePasswordRequest.NewPassword);
                    await this._unitOfWork.CompleteAsync();
                    return OkResponse(new ChangePasswordResponse(Messages.Success), ResultCodeConstants.ChangeSuccess);
                }
                return ErrorResponse(ResultCodeConstants.PasswordWrong);

            }
            return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.Password});

        }


        public async Task<APIResponse> ValidateAnswer(ValidateAnswerRequest answerRequest)
        {
            var customer = await _customerRepository.FindByPhone(answerRequest.Phone);
            var qId1 = customer.Questions.Where(x => x.QuestionId == answerRequest.QuestionId1).FirstOrDefault();
            var qId2 = customer.Questions.Where(x => x.QuestionId == answerRequest.QuestionId2).FirstOrDefault();
            IDictionary<string, object> customerInfo = new Dictionary<string, object>();
            customerInfo.Add("Id", customer.Id.ToString());
            string token = JwtHelper.GenerateToken(_jwtConfig, customerInfo);
            var response = new ValidateAnswerResponse
            {
                Token = token
            };
            if (!Validation.IsEqualOrLessThanZero(answerRequest.QuestionId1) &&
                !Validation.IsNullOrEmpty(answerRequest.Answer1) &&
                !Validation.IsEqualOrLessThanZero(answerRequest.QuestionId2) &&
                !Validation.IsNullOrEmpty(answerRequest.Answer2))
            {
                if (qId1.Answer == answerRequest.Answer1 && qId2.Answer == answerRequest.Answer2)
                {
                    return OkResponse(response, ResultCodeConstants.Success);
                }
                return ErrorResponse(ResultCodeConstants.AnswerWrong);

            }
            return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { Messages.Answer });
        }

        public async Task<APIResponse> ValidateOtp(OTPValidateRequest oTPValidate)
        {
            var otp = "123456";
            var customer = await _customerRepository.FindByPhone(oTPValidate.Phone);
            IDictionary<string, object> customerInfo = new Dictionary<string, object>();
            customerInfo.Add("Id", customer.Id.ToString());
            string token = JwtHelper.GenerateToken(_jwtConfig, customerInfo);
            var response = new ValidateAnswerResponse
            {
                Token = token
            };
            if (oTPValidate.Phone == null || oTPValidate.Phone == "")
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] {Messages.Phone });

            if (!Validation.IsValidPhone(oTPValidate.Phone))
                return ErrorResponse(ResultCodeConstants.EInvalidPhoneFormat);

            if (otp != oTPValidate.OtpCode)
                return ErrorResponse(ResultCodeConstants.AuthEInvalidOTP);

            return OkResponse(response, ResultCodeConstants.AuthValidOTP);
        }
    }
}