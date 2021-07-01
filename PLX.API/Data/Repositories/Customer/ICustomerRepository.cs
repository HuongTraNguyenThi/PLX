using System;
using System.Threading.Tasks;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> FindByPhone(string phone, bool active = true);
        Task<Customer> FindById(int id, bool active = true);
    }
}