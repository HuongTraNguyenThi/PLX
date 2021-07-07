using System;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<Customer> FindByIdCustomer(int id);
    }
}