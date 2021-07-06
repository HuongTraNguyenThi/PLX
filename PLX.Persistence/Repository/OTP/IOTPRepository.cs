using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface IOTPRepository : IRepository<OTP>
    {
        Task<OTP> FindByPhone(string phone, bool active = true);
        Task<List<OTP>> ListByPhone(string phone, bool active = true);
        Task<List<OTP>> FindByPhoneAndCode(string phone, string otpCode, bool active = true);
    }
}