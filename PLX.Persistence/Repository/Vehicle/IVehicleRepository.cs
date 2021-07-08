using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<List<Vehicle>> FindByIdCustomer(int customerId, bool active = true);

    }
}