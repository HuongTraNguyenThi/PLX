using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;


namespace PLX.API.Services
{
    public interface ICustomerService
    {

        Task<APIResponse> RegisterAsync(CustomerRegister customer);
        Task<APIResponse> GetLists(BaseRequest baseRequest);
        Task<APIResponse> GetListDistricts(BaseRequest baseRequest, int proviceId);
        Task<APIResponse> GetListWards(BaseRequest baseRequest, int districtId);
        Task<APIResponse> GetLists();
        Task<APIResponse> GetListDistricts(int proviceId);
        Task<APIResponse> GetListWards(int districtId);
        Task<APIResponse> GetCustomerById(BaseRequest baseRequest, int id);
    }
}