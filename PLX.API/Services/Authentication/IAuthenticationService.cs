using System;
using System.Threading.Tasks;
using PLX.API.Data.Models;
using PLX.API.Data.DTO.Customer;
using PLX.API.Data.DTO;

namespace PLX.API.Services
{
    public interface IAuthenticationService
    {
        Task<APIResponse> Authenticate(AuthenticationRequest authRequest);
        Task<APIResponse> FindUserById(int id);
        Task<APIResponse> GenerateOTP(OTPDTO oTPDTO);
        Task<APIResponse> ValidateOTP(string phone, int otp);
    }
}