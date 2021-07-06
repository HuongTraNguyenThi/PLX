using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.EF.Context;
using PLX.Persistence.Model;
using PLX.Persistence.Repository;

namespace PLX.Persistence.EF.Repository
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
        public async Task<Customer> FindByPhone(string phone)
        {
            return await this._dbSet.Where(x => x.Phone == phone).FirstOrDefaultAsync();
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