using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.EF.Context;
using PLX.Persistence.Model;
using PLX.Persistence.Repository;

namespace PLX.Persistence.EF.Repository
{
    public class CustomerQuestionRepository : AbstractRepository<CustomerQuestion>, ICustomerQuestionRepository
    {
        public CustomerQuestionRepository(PLXDbContext context) : base(context)
        {

        }
    }
}