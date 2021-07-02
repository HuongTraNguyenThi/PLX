using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Contexts;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public class CustomerRepository : AbstractRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(PLXDbContext context) : base(context)
        {
        }

        public async Task<Customer> FindByPhone(string phone, bool active = true)
        {
            var queryable = this._dbSet.Where(customer => customer.Phone == phone && customer.Active == active)
                .Include(x => x.Vehicles)
                .Include(x => x.LinkedCards)
                .Include(x => x.Questions);
            return await queryable.FirstOrDefaultAsync();
        }
        public async Task<Customer> FindById(int id, bool active = true)
        {
            var queryable = this._dbSet.Where(customer => customer.Id == id && customer.Active == active)
                .Include(x => x.Vehicles)
                .Include(x => x.LinkedCards)
                .Include(x => x.Questions);
            return await queryable.FirstOrDefaultAsync();
        }
    }
}