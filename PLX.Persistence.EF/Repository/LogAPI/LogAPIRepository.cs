using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.EF.Context;
using PLX.Persistence.Model;
using PLX.Persistence.Repository;

namespace PLX.Persistence.EF.Repository
{
    public class LogAPIRepository : AbstractRepository<LogAPI>, ILogAPIRepository
    {
        public LogAPIRepository(PLXDbContext context) : base(context)
        {
        }
    }
}