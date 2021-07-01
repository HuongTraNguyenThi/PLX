using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Contexts;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public class WardRepository : AbstractRepository<Ward>, IWardRepository
    {
        public WardRepository(PLXDbContext context) : base(context)
        {
        }
    }
}