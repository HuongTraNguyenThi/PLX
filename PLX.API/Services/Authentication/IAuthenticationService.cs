using System;
using System.Threading.Tasks;
using PLX.API.Data.DTO.Authentication;
using PLX.API.Data.DTO;

namespace PLX.API.Services
{
    public interface IAuthenticationService
    {
        Task<APIResponse> Authenticate(AuthenticationRequest authRequest);
        Task<APIResponse> FindUserById(int id);
        Task<APIResponse> GenerateOTP(OTPGenerateRequest oTPRequest);
        APIResponse ValidateOTP(OTPValidateRequest oTPRequest);
    }
}
