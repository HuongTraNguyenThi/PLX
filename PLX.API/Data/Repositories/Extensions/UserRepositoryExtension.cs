using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public static class UserRepositoryExtension
    {
        public static async Task<Customer> FindCustomerByPhoneAndPasword(this IRepository<Customer> customerRepository, string phone)
        {
            var customer = await customerRepository.Entities.Where(user => user.Phone == phone).FirstOrDefaultAsync();
            return customer;
        }

    }
}