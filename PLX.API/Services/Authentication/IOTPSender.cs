using System;
using System.Threading.Tasks;
using PLX.API.Data.Models;
using PLX.API.Data.DTO.Customer;
using PLX.API.Data.DTO;

namespace PLX.API.Services
{
    public interface IOTPSender
    {
        Task<APIResponse> SendSMS(string phone, int otp);
    }
}