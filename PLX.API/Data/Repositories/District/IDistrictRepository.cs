using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task<List<District>> FindByProvinceId(int provinceId);
    }
}