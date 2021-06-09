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
            IRepository<District> districtRepository, IRepository<Ward> wardRepository) {
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
        public async Task<Customer> FindById(int id) {
            var customer = await _customerRepository.FindAsync(id);
            return customer;
        }
        public async Task<BaseResponse<Customer>> AddAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.CompleteAsync();
            return new BaseResponse<Customer>(customer);
        }
        public async Task<BaseResponse<Customer>> UpdateAsync(int id, Customer customer)
        {
            _customerRepository.Update(customer);
            await _unitOfWork.CompleteAsync();
            return new BaseResponse<Customer>(customer);
        }
        public async Task<BaseResponse<Customer>> DeleteAsync(int id)
        {
            var customer = await _customerRepository.FindAsync(id);
            _customerRepository.Remove(customer);
            await _unitOfWork.CompleteAsync();
            return new BaseResponse<Customer>(customer);
        }
         public async Task<CustomerResponse> RegisterAsync(CustomerRegister customerRegister)
        {
            var result = new CustomerResponse();
            var customer = _mapper.Map<CustomerRegister, Customer>(customerRegister);

            if(customer.CustomerTypeId == 1 )
            {
                if(customer.Name == null || customer.Name == "") 
                {
                    result.ErrorMessage = "Tên không được trống";
                    return result;
                } 
                if(customer.Phone == null || customer.Phone == "") 
                {
                    result.ErrorMessage = "Số điện thoại không được trống";
                    return result;
                } 
                if(customer.Password == null || customer.Password == "") 
                {
                    result.ErrorMessage = "Mật khẩu không được trống";
                    return result;
                } 
                
                foreach (var item in customer.Questions)
                {
                    if(item.Question.Content == null || item.Question.Content == "")
                    {
                        result.ErrorMessage = "Câu hỏi bí mật không được trống";
                        return result;
                    }
                        
                }
                
                if(customer.Date == null || customer.Date == "") 
                {
                    result.ErrorMessage = "Ngày sinh không được trống";
                    return result;
                } 
                if(customer.ProvinceId == 0) 
                {
                    result.ErrorMessage = "Tỉnh không được trống";
                    return result;
                } 
                if(customer.DistrictId == 0) 
                {
                    result.ErrorMessage = "Quận/Huyện không được trống";
                    return result;
                } 
                if(customer.WardId == 0) 
                {
                    result.ErrorMessage = "Phường/Xã không được trống";
                    return result;
                } 
                if(customer.Address == null || customer.Address == "") 
                {
                    result.ErrorMessage = "Địa chỉ không được trống";
                    return result;
                } 
                if(customer.CardID == null || customer.CardID == "") 
                {
                    result.ErrorMessage = "Số CMND/CCCD không được trống";
                    return result;
                } 
                if(customer.Gender == null || customer.Gender == "") 
                {
                    result.ErrorMessage = "Giới tính không được trống";
                    return result;
                } 
            }
            if(customer.CustomerTypeId == 2 )
            {
                if(customer.Name == null || customer.Name == "") 
                {
                    result.ErrorMessage = "Tên không được trống";
                    return result;
                } 
                if(customer.Phone == null || customer.Phone == "") 
                {
                    result.ErrorMessage = "Số điện thoại không được trống";
                    return result;
                } 
                if(customer.Password == null || customer.Password == "") 
                {
                    result.ErrorMessage = "Mật khẩu không được trống";
                    return result;
                } 
                foreach (var item in customer.Questions)
                {
                    if(item.Question.Content == null || item.Question.Content == "")
                    {
                        result.ErrorMessage = "Câu hỏi bí mật không được trống";
                        return result;
                    }
                }
                if(customer.Date == null || customer.Date == "") 
                {
                    result.ErrorMessage = "Ngày sinh không được trống";
                    return result;
                } 
                 if(customer.ProvinceId == 0 ) 
                {
                    result.ErrorMessage = "Tỉnh không được trống";
                    return result;
                } 
                if(customer.DistrictId == 0) 
                {
                    result.ErrorMessage = "Quận/Huyện không được trống";
                    return result;
                } 
                if(customer.WardId == 0) 
                {
                    result.ErrorMessage = "Phường/Xã không được trống";
                    return result;
                } 
                if(customer.Address == null || customer.Address == "")
                {
                    result.ErrorMessage = "Địa chỉ không được trống";
                    return result;
                } 
                if(customer.Email == null || customer.Email == "") 
                {
                    result.ErrorMessage = "Email không được trống";
                    return result;
                } 
                if(customer.TaxCode == null || customer.TaxCode == "") 
                {
                    result.ErrorMessage = "Mã số thuế không được trống";
                    return result;
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
            
            result.IdCustomer = customer.Id;
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
            var customerStaticList = new CustomerStaticList{
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
            return districtList ;
        }

        public async Task<List<ListItem>> GetListWards(int districtId)
        {
            var all = await _wardRepository.ListAsync();
            var wards = all.Where(x => x.DistrictId == districtId).ToList();
            var wardList = _mapper.Map<List<Ward>, List<ListItem>>(wards);
            return wardList ;
        }
    }
}