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
    public class LinkedCardRepository : AbstractRepository<LinkedCard>, ILinkedCardRepository
    {
        public LinkedCardRepository(PLXDbContext context) : base(context)
        {
        }

        public override void Remove(LinkedCard linkedCard)
        {
            linkedCard.Active = false;
            _dbSet.Update(linkedCard);
        }
    }
}