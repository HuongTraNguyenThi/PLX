using System.Threading.Tasks;
using PLX.API.Data.Contexts;

namespace PLX.API.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork {
        private readonly PLXDbContext _context;
        public UnitOfWork(PLXDbContext context) {
            _context = context;
        }
        public async Task CompleteAsync() {
            await _context.SaveChangesAsync();
        }
    }
}