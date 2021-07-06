using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.EF.Context;
using PLX.Persistence.Model;
using PLX.Persistence.Repository;

namespace PLX.Persistence.EF.Repository
{
    public class OTPRepository : AbstractRepository<OTP>, IOTPRepository
    {
        public OTPRepository(PLXDbContext context) : base(context)
        {
        }

        public async Task<OTP> FindByPhone(string phone, bool active = true)
        {
            return await this._dbSet.Where(x => x.Phone == phone && x.Active == active).FirstOrDefaultAsync();
        }

        public async Task<List<OTP>> FindByPhoneAndCode(string phone, string otpCode, bool active = true)
        {
            return await this._dbSet.Where(x => x.Phone == phone && x.OTPCode == otpCode && x.Active == active).ToListAsync();
        }

        public async Task<List<OTP>> ListByPhone(string phone, bool active = true)
        {
            return await this._dbSet.Where(x => x.Phone == phone && x.Active == active).ToListAsync();
        }
    }
}