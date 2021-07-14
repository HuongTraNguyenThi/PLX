using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface ICustomerQuestionRepository : IRepository<CustomerQuestion>
    {
        Task<List<CustomerQuestion>> FindByPhone(string phone);

    }
}