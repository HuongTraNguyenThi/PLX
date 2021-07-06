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
    public class WardRepository : AbstractRepository<Ward>, IWardRepository
    {
        public WardRepository(PLXDbContext context) : base(context)
        {
        }
        public async Task<List<Ward>> FindByDistrictId(int districtId)
        {
            return await this._dbSet.Where(ward => ward.DistrictId == districtId).ToListAsync();
        }
    }
}