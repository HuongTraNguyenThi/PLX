using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public static class UserRepositoryExtension
    {
        public static async Task<Customer> FindCustomerByPhone(this IRepository<Customer> customerRepository, string phone)
        {
            var customer = await customerRepository.Entities
                .Where(user => user.Phone == phone && user.Active == true)
                .Include(x => x.Vehicles)
                .Include(x => x.LinkedCards)
                .Include(x => x.Questions)
                .FirstOrDefaultAsync();
            return customer;
        }

    }
}