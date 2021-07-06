using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface IWardRepository : IRepository<Ward>
    {
        Task<List<Ward>> FindByDistrictId(int districtId);
    }
}