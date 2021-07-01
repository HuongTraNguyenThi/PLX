using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
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
            var isValidPhone = Validation.CheckPhone(customerRegister.CustomerInfo.CustomerBasic.Phone);
            var isValidDate = Validation.CheckDate(customerRegister.CustomerInfo.CustomerCard.Date);


            if (customerRegister.CustomerInfo.CustomerBasic.CustomerTypeId == 1)
            {
                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerBasic.Name)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Tên" });
                }
                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerBasic.Phone)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Số điện thoại" });
                }
                if (!isValidPhone)
                    return ErrorResponse(ResultCodeConstants.ErrorValidePhone);
                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerBasic.Password)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Mật khẩu" });
                }

                foreach (var item in customerRegister.CustomerInfo.CustomerBasic.Questions)
                {
                    if (!Validation.IsNullOrEmpty(item.QuestionId))
                    {
                        return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Câu hỏi bí mật" });
                    }
                }

                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.Date)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Ngày sinh" });
                }
                if (!isValidDate)
                    return ErrorResponse(ResultCodeConstants.ErrorValideDate, new object[] { "Ngày sinh" });

                if (!Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.ProvinceId))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Tỉnh" });
                }

                if (!Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.DistrictId))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Quận/Huyện" });
                }

                if (!Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.WardId))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Phường/Xã" });
                }

                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.Address)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Địa chỉ liên hệ" });
                }

                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.CardId)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Số CMND/CCCD" });
                }

                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.Gender)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Giới tính" });
                }
            }

            if (customerRegister.CustomerInfo.CustomerBasic.CustomerTypeId == 2)
            {
                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerBasic.Name)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Tên đơn vị" });
                }
                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerBasic.Phone)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Số điện thoại" });
                }
                if (!isValidPhone)
                    return ErrorResponse(ResultCodeConstants.ErrorValideDate);
                if (customerRegister.CustomerInfo.CustomerBasic.Password == null || customerRegister.CustomerInfo.CustomerBasic.Password == "")
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Mật khẩu" });
                }
                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.Date)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Ngày thành lập" });
                }
                if (!isValidDate)
                    return ErrorResponse(ResultCodeConstants.ErrorValideDate, new object[] { "Ngày thành lập" });
                foreach (var item in customerRegister.CustomerInfo.CustomerBasic.Questions)
                {
                    if (!Validation.IsNullOrEmpty(item.QuestionId))
                    {
                        return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Câu hỏi bí mật" });
                    }
                }
                if (!Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.ProvinceId))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Tỉnh" });
                }
                if (!Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.DistrictId))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Quận/Huyện" });
                }
                if (!Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.WardId))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Phường/Xã" });
                }
                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.Address)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Địa chỉ liên hệ" });
                }
                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerBasic.Email)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Email" });
                }
                if (!(Validation.IsNullOrEmpty(customerRegister.CustomerInfo.CustomerCard.TaxCode)))
                {
                    return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Mã số thuế" });
                }
            }
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

        //////
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


    }
}