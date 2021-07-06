using System.Threading.Tasks;
using PLX.Persistence.EF.Context;
using PLX.Persistence.Repository;

namespace PLX.Persistence.EF.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PLXDbContext _context;
        public UnitOfWork(PLXDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}