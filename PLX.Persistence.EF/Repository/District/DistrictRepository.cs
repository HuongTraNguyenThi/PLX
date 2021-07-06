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
    public class DistrictRepository : AbstractRepository<District>, IDistrictRepository
    {
        public DistrictRepository(PLXDbContext context) : base(context)
        {
        }

        public async Task<List<District>> FindByProvinceId(int provinceId)
        {
            return await this._dbSet.Where(district => district.ProvinceId == provinceId).ToListAsync();
        }
    }
}