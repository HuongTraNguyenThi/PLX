using System;
using System.Threading.Tasks;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> FindByPhone(string phone, bool active = true);
        Task<Customer> FindByPhone(string phone);
        Task<Customer> FindById(int id, bool active = true);
    }
}