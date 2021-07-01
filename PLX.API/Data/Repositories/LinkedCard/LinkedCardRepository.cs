using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Contexts;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public class LinkedCardRepository : AbstractRepository<LinkedCard>, ILinkedCardRepository
    {
        public LinkedCardRepository(PLXDbContext context) : base(context)
        {
        }
    }
}