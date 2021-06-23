using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;


namespace PLX.API.Services
{
    public interface IVehicleService
    {
        Task<APIResponse> GetListVehicleType(BaseRequest baseRequest);

    }
}