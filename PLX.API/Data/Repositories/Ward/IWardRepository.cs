using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public interface IWardRepository : IRepository<Ward>
    {
        Task<List<Ward>> FindByDistrictId(int districtId);
    }
}