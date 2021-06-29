using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public static class OTPReponsitoryExtension
    {
        public static async Task<OTP> FindOTPByPhone(this IRepository<OTP> otpRepository, string phone)
        {
            var otp = await otpRepository.Entities.Where(x => x.Phone == phone).FirstOrDefaultAsync();
            return otp;
        }
        public static async Task<List<OTP>> FindOTPByPhoneAndActive(this IRepository<OTP> otpRepository, string phone)
        {
            var otp = await otpRepository.Entities.Where(x => x.Phone == phone && x.Active == true).ToListAsync();
            return otp;
        }
        public static async Task<List<OTP>> FindOTPByPhoneAndOTP(this IRepository<OTP> otpRepository, string phone, string otpCode = "123456")
        {
            var otp = await otpRepository.Entities.Where(x => x.Phone == phone && x.Active == true && x.OTPCode == otpCode).ToListAsync();
            return otp;
        }
    }
}
