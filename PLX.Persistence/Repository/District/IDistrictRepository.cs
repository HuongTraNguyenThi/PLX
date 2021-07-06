using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task<List<District>> FindByProvinceId(int provinceId);
    }
}