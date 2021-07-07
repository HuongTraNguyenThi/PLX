using System;
using System.Threading.Tasks;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<Customer> FindByIdCustomer(int id);
    }
}