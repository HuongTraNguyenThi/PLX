using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;
using PLX.API.Data.Models;
using PLX.API.Data.Repositories;

namespace PLX.API.Services
{

    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<LinkedCard> _linkedCardRepository;
        private readonly IRepository<CustomerQuestion> _customerQuestionsRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Ward> _wardRepository;
        private readonly IRepository<Question> _questionsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper,
            IRepository<Customer> customerRepository, IRepository<Vehicle> vehicleRepository,
            IRepository<LinkedCard> linkedCardRepository, IRepository<CustomerQuestion> customerQuestionsRepository,
            IRepository<Question> questionsRepository, IRepository<Province> provinceRepository,
            IRepository<District> districtRepository, IRepository<Ward> wardRepository)
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
            return new APIResponse(customerDTO);
        }
        public async Task<APIResponse> UpdateAsync(int id, CustomerDTO customerDTO)
        {
            var cus = _mapper.Map<CustomerDTO, Customer>(customerDTO);
            _customerRepository.Update(cus);
            await _unitOfWork.CompleteAsync();
            return new APIResponse(customerDTO);
        }
        public async Task<APIResponse> DeleteAsync(int id)
        {

            var customer = await _customerRepository.FindAsync(id);
            var cus = _mapper.Map<Customer, CustomerDTO>(customer);
            _customerRepository.Remove(customer);
            await _unitOfWork.CompleteAsync();
            return new APIResponse(cus);
        }
        public async Task<APIResponse> RegisterAsync(CustomerRegister customerRegister)
        {


            var customer = _mapper.Map<CustomerRegister, Customer>(customerRegister);

            if (customer.CustomerTypeId == 1)
            {
                if (customer.Name == null || customer.Name == "")
                {
                    return new APIResponse(1, "Tên không được trống");

                }
                if (customer.Phone == null || customer.Phone == "")
                {
                    return new APIResponse(1, "Số điện thoại không được trống");

                }
                if (customer.Password == null || customer.Password == "")
                {

                    return new APIResponse(1, "Mật khẩu không được trống");

                }

                foreach (var item in customer.Questions)
                {
                    if (item.Question.Content == null || item.Question.Content == "")
                    {
                        return new APIResponse(1, "Câu hỏi bí mật không được trống");

                    }

                }

                if (customer.Date == null)
                {
                    return new APIResponse(1, "Ngày sinh không được trống");

                }
                if (customer.ProvinceId == 0)
                {
                    return new APIResponse(1, "Tỉnh không được trống");

                }
                if (customer.DistrictId == 0)
                {
                    return new APIResponse(1, "Quận/Huyện không được trống");

                }
                if (customer.WardId == 0)
                {
                    return new APIResponse(1, "Phường/Xã không được trống");

                }
                if (customer.Address == null || customer.Address == "")
                {
                    return new APIResponse(1, "Địa chỉ không được trống");

                }
                if (customer.CardID == null || customer.CardID == "")
                {
                    return new APIResponse(1, "Số CMND/CCCD không được trống");

                }
                if (customer.Gender == null || customer.Gender == "")
                {
                    return new APIResponse(1, "Giới tính không được trống");

                }
            }
            if (customer.CustomerTypeId == 2)
            {
                if (customer.Name == null || customer.Name == "")
                {
                    return new APIResponse(1, "Tên không được trống");

                }
                if (customer.Phone == null || customer.Phone == "")
                {
                    return new APIResponse(1, "Số điện thoại không được trống");

                }
                if (customer.Password == null || customer.Password == "")
                {

                    return new APIResponse(1, "Mật khẩu không được trống");

                }

                foreach (var item in customer.Questions)
                {
                    if (item.Question.Content == null || item.Question.Content == "")
                    {
                        return new APIResponse(1, "Câu hỏi bí mật không được trống");

                    }

                }
                if (customer.Date == null)
                {
                    return new APIResponse(1, "Ngày thành lập không được trống");

                }
                if (customer.ProvinceId == 0)
                {
                    return new APIResponse(1, "Tỉnh không được trống");

                }
                if (customer.DistrictId == 0)
                {
                    return new APIResponse(1, "Quận/Huyện không được trống");

                }
                if (customer.WardId == 0)
                {
                    return new APIResponse(1, "Phường/Xã không được trống");

                }
                if (customer.Address == null || customer.Address == "")
                {
                    return new APIResponse(1, "Địa chỉ không được trống");

                }
                if (customer.Email == null || customer.Email == "")
                {
                    return new APIResponse(1, "Email không được trống");

                }
                if (customer.TaxCode == null || customer.TaxCode == "")
                {
                    return new APIResponse(1, "Mã số thuế không được trống");

                }
            }
            var vehicles = _mapper.Map<List<VehicleDTO>, List<Vehicle>>(customerRegister.Vehicles);
            vehicles.ForEach(vehicle => vehicle.Customer = customer);
            var linkedCards = _mapper.Map<List<LinkedCardDTO>, List<LinkedCard>>(customerRegister.LinkedCards);
            linkedCards.ForEach(linkedCard => linkedCard.Customer = customer);

            var questions = _mapper.Map<List<QuestionDTO>, List<CustomerQuestion>>(customerRegister.CustomerInfo.CustomerBasic.Questions);
            questions.ForEach(questions => questions.Customer = customer);

            await _customerRepository.AddAsync(customer);

            await _vehicleRepository.AddRangeAsync(vehicles);

            await _linkedCardRepository.AddRangeAsync(linkedCards);

            await _customerQuestionsRepository.AddRangeAsync(questions);

            await _unitOfWork.CompleteAsync();
            var result = new APIResponse(new CustomerResponse()
            {
                IdCustomer = customer.Id
            });
            return result;
        }

        public async Task<CustomerStaticList> GetLists()
        {
            var questions = await _questionsRepository.ListAsync();
            var provinces = await _provinceRepository.ListAsync();
            var questionList = _mapper.Map<List<Question>, List<ListItem>>(questions);
            var provinceList = _mapper.Map<List<Province>, List<ListItem>>(provinces);
            var genderList = new List<ListItem>();
            genderList.Add(new ListItem("male", "Nam"));
            genderList.Add(new ListItem("female", "Nu"));
            var customerStaticList = new CustomerStaticList
            {
                Questions = questionList,
                Provinces = provinceList,
                Genders = genderList
            };
            return customerStaticList;
        }
        public async Task<List<ListItem>> GetListDistricts(int provinceId)
        {
            var all = await _districtRepository.ListAsync();
            var districts = all.Where(x => x.ProvinceId == provinceId).ToList();
            var districtList = _mapper.Map<List<District>, List<ListItem>>(districts);
            return districtList;
        }

        public async Task<List<ListItem>> GetListWards(int districtId)
        {
            var all = await _wardRepository.ListAsync();
            var wards = all.Where(x => x.DistrictId == districtId).ToList();
            var wardList = _mapper.Map<List<Ward>, List<ListItem>>(wards);
            return wardList;
        }
    }
}