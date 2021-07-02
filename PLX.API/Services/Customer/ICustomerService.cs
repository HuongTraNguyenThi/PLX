using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;


namespace PLX.API.Services
{
    public interface ICustomerService
    {
        Task<APIResponse> RegisterAsync(CustomerRegister customer);
        Task<APIResponse> GetStaticLists();
        Task<APIResponse> GetDistrictsByProvinceId(int proviceId);
        Task<APIResponse> GetWardsByDistrictId(int districtId);
        Task<APIResponse> GetCustomerById(BaseRequest baseRequest, int id);
        Task<APIResponse> UpdateCustomer(int id, CustomerUpdateRequest customerUpdateRequest);
    }
}