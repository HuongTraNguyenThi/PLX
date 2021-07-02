using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public interface IOTPRepository : IRepository<OTP>
    {
        Task<OTP> FindByPhone(string phone, bool active = true);
        Task<List<OTP>> ListByPhone(string phone, bool active = true);
        Task<List<OTP>> FindByPhoneAndCode(string phone, string otpCode, bool active = true);
    }
}