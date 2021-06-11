using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;


namespace PLX.API.Services
{
    public interface ICustomerService
    {

        Task<APIResponse> RegisterAsync(CustomerRegister customer);
        Task<CustomerStaticList> GetLists();
        Task<List<ListItem>> GetListDistricts(int proviceId);
        Task<List<ListItem>> GetListWards(int districtId);
        Task<CustomerDTO> GetCustomerById(int id);
    }
}