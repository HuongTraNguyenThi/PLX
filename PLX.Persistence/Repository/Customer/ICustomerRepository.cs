using System;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> FindByPhone(string phone, bool active = true);
        Task<Customer> FindById(int id, bool active = true);

    }
}