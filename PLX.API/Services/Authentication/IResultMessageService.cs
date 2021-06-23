using System;
using System.Threading.Tasks;
using PLX.API.Data.Models;
using PLX.API.Data.DTO.Customer;
using PLX.API.Data.DTO;

namespace PLX.API.Services
{
    public interface IResultMessageService
    {
        Task<string> GetMessage(string resultCode, params object[] arguments);
    }
}