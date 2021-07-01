using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PLX.API.Constants;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;
using PLX.API.Data.Models;
using PLX.API.Data.Repositories;
using PLX.API.Helpers;


namespace PLX.API.Services
{

    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<LinkedCard> _linkedCardRepository;
        private readonly IRepository<CustomerQuestion> _customerQuestionsRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Ward> _wardRepository;
        private readonly IRepository<Question> _questionsRepository;
        private readonly IResultMessageService _iResultMessageService;

        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;


        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper,
            IRepository<Customer> customerRepository, IRepository<Vehicle> vehicleRepository,
            IRepository<LinkedCard> linkedCardRepository, IRepository<CustomerQuestion> customerQuestionsRepository,
            IRepository<Question> questionsRepository, IRepository<Province> provinceRepository,
            IRepository<District> districtRepository, IRepository<Ward> wardRepository, IResultMessageService iResultMessageService)
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
            _iResultMessageService = iResultMessageService;
        }

        public async Task<List<Customer>> ListAsync()
        {
            var customerList = await _customerRepository.ListAsync();
            return customerList;
        }
        public async Task<Customer> FindById(int id)
        {
            var customer = await _customerRepository.FindAsync(id);
            return customer;
        }
        public async Task<APIResponse> AddAsync(CustomerDTO customerDTO)
        {
            var cus = _mapper.Map<CustomerDTO, Customer>(customerDTO);
            await _customerRepository.AddAsync(cus);
            await _unitOfWork.CompleteAsync();
            return OkResponse(customerDTO);
        }
        public async Task<APIResponse> UpdateAsync(int id, CustomerDTO customerDTO)
        {
            var cus = _mapper.Map<CustomerDTO, Customer>(customerDTO);
            _customerRepository.Update(cus);
            await _unitOfWork.CompleteAsync();
            return OkResponse(customerDTO);
        }
        public async Task<APIResponse> DeleteAsync(int id)
        {

            var customer = await _customerRepository.FindAsync(id);
            var cus = _mapper.Map<Customer, CustomerDTO>(customer);
            _customerRepository.Remove(customer);
            await _unitOfWork.CompleteAsync();
            return OkResponse(cus);
        }
        public async Task<APIResponse> RegisterAsync(CustomerRegister customerRegister)
        {
            var error = validateCustomer(customerRegister.CustomerInfo);
            if (error != null) return error;

            var customer = _mapper.Map<CustomerRegister, Customer>(customerRegister);
            var vehicles = _mapper.Map<List<VehicleRequest>, List<Vehicle>>(customerRegister.Vehicles);
            vehicles.ForEach(vehicle => vehicle.Customer = customer);
            var linkedCards = _mapper.Map<List<LinkedCardRequest>, List<LinkedCard>>(customerRegister.LinkedCards);
            linkedCards.ForEach(linkedCard => linkedCard.Customer = customer);

            var questions = _mapper.Map<List<QuestionDTO>, List<CustomerQuestion>>(customerRegister.CustomerInfo.CustomerBasic.Questions);
            questions.ForEach(questions => questions.Customer = customer);

            await _customerRepository.AddAsync(customer);

            await _vehicleRepository.AddRangeAsync(vehicles);

            await _linkedCardRepository.AddRangeAsync(linkedCards);

            await _customerQuestionsRepository.AddRangeAsync(questions);

            await _unitOfWork.CompleteAsync();
            var result = OkResponse(new CustomerResponse()
            {
                IdCustomer = customer.Id
            }, ResultCodeConstants.SuccessRegister);
            return result;
        }

        public async Task<APIResponse> GetLists(BaseRequest baseRequest)
        {
            var questions = await _questionsRepository.ListAsync();
            var provinces = await _provinceRepository.ListAsync();
            var questionList = _mapper.Map<List<Question>, List<ListItem>>(questions);
            var provinceList = _mapper.Map<List<Province>, List<ListItem>>(provinces);
            var genderList = new List<ListItem>();
            genderList.Add(new ListItem("0", "Nam"));
            genderList.Add(new ListItem("1", "Nữ"));
            genderList.Add(new ListItem("Other", "Khác"));
            var customerStaticList = new CustomerStaticList
            {
                Questions = questionList,
                Provinces = provinceList,
                Genders = genderList
            };
            return new ApiOkResponse(customerStaticList, ResultCodeConstants.Success);
        }
        public async Task<APIResponse> GetListDistricts(BaseRequest baseRequest, int provinceId)
        {
            var all = await _districtRepository.ListAsync();
            var districts = all.Where(x => x.ProvinceId == provinceId).ToList();
            var districtList = _mapper.Map<List<District>, List<ListItem>>(districts);
            var result = new DistrictDTO
            {
                Districts = districtList
            };
            return new ApiOkResponse(result, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetListWards(BaseRequest baseRequest, int districtId)
        {
            var all = await _wardRepository.ListAsync();
            var wards = all.Where(x => x.DistrictId == districtId).ToList();
            var wardList = _mapper.Map<List<Ward>, List<ListItem>>(wards);
            var result = new WardDTO
            {
                Wards = wardList
            };
            return new ApiOkResponse(result, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetLists()
        {

            var questions = await _questionsRepository.ListAsync();
            var provinces = await _provinceRepository.ListAsync();
            var questionList = _mapper.Map<List<Question>, List<ListItem>>(questions);
            var provinceList = _mapper.Map<List<Province>, List<ListItem>>(provinces);
            var genderList = new List<ListItem>();
            genderList.Add(new ListItem("0", "Nam"));
            genderList.Add(new ListItem("1", "Nữ"));
            genderList.Add(new ListItem("Other", "Khác"));
            var customerStaticList = new CustomerStaticList
            {
                Questions = questionList,
                Provinces = provinceList,
                Genders = genderList
            };
            return new ApiOkResponse(customerStaticList, ResultCodeConstants.Success);
        }
        public async Task<APIResponse> GetListDistricts(int provinceId)
        {
            var all = await _districtRepository.ListAsync();
            var districts = all.Where(x => x.ProvinceId == provinceId).ToList();
            var districtList = _mapper.Map<List<District>, List<ListItem>>(districts);
            var result = new DistrictDTO
            {
                Districts = districtList
            };
            return new ApiOkResponse(result, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetListWards(int districtId)
        {
            var all = await _wardRepository.ListAsync();
            var wards = all.Where(x => x.DistrictId == districtId).ToList();
            var wardList = _mapper.Map<List<Ward>, List<ListItem>>(wards);
            var result = new WardDTO
            {
                Wards = wardList
            };
            return new ApiOkResponse(result, ResultCodeConstants.Success);
        }

        public async Task<APIResponse> GetCustomerById(BaseRequest baseRequest, int id)
        {
            var all = await _customerRepository.FindAsync(id);
            var customerDto = _mapper.Map<Customer, CustomerDTO>(all);
            return new ApiOkResponse(customerDto, ResultCodeConstants.Success);
        }

        private ApiErrorResponse validateCustomer(CustomerInfo customerInfo)
        {
            var customerBasic = customerInfo.CustomerBasic;
            var isBizCustomer = customerBasic.CustomerTypeId == CustomerTypes.BizCustomer;

            if (!Validation.IsNullOrEmpty(customerBasic.Name))
            {
                var argurments = new object[] { isBizCustomer ? "Tên đơn vị" : "Tên" };
                return ErrorResponse(ResultCodeConstants.ErrorRegister, argurments);
            }

            if (!Validation.IsNullOrEmpty(customerBasic.Phone))
            {
                return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Số điện thoại" });
            }

            if (!Validation.IsValidPhone(customerBasic.Phone))
            {
                return ErrorResponse(ResultCodeConstants.ErrorInvalidPhone);
            }

            if (!Validation.IsNullOrEmpty(customerBasic.Password))
            {
                return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Mật khẩu" });
            }

            var customerCard = customerInfo.CustomerCard;
            if (!(Validation.IsNullOrEmpty(customerCard.Date)))
            {
                var argurments = new object[] { isBizCustomer ? "Ngày thành lập" : "Ngày sinh" };
                return ErrorResponse(ResultCodeConstants.ErrorRegister, argurments);
            }

            foreach (var item in customerBasic.Questions)
            {
                if (!Validation.IsNonZero(item.QuestionId))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Câu hỏi bí mật" });
                }
            }

            var isValidDate = Validation.IsValidDate(customerCard.Date);
            if (!isValidDate)
            {
                var argurments = new object[] { isBizCustomer ? "Ngày thành lập" : "Ngày sinh" };
                return ErrorResponse(ResultCodeConstants.ErrorInvalidDate, argurments);
            }

            if (!Validation.IsNonZero(customerCard.ProvinceId))
            {
                return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Tỉnh" });
            }

            if (!Validation.IsNonZero(customerCard.DistrictId))
            {
                return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Quận/Huyện" });
            }

            if (!Validation.IsNonZero(customerCard.WardId))
            {
                return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Phường/Xã" });
            }

            if (!(Validation.IsNullOrEmpty(customerCard.Address)))
            {
                return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Địa chỉ liên hệ" });
            }

            if (isBizCustomer)
            {
                if (!(Validation.IsNullOrEmpty(customerBasic.Email)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Email" });
                }
                if (!(Validation.IsNullOrEmpty(customerCard.TaxCode)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Mã số thuế" });
                }
            }
            else
            {
                if (!(Validation.IsNullOrEmpty(customerCard.CardId)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Số CMND/CCCD" });
                }

                if (!(Validation.IsNullOrEmpty(customerCard.Gender)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Giới tính" });
                }
            }

            return null;
        }
    }
}