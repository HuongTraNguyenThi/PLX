using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface ILinkedCardRepository : IRepository<LinkedCard>
    {
        Task<List<LinkedCard>> FindByIdCustomer(int id);
    }
}