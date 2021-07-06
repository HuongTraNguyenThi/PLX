using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.EF.Context;
using PLX.Persistence.Model;
using PLX.Persistence.Repository;

namespace PLX.Persistence.EF.Repository
{
    public class ApiResultRepository : AbstractRepository<Result>, IApiResultRepository
    {
        public ApiResultRepository(PLXDbContext context) : base(context)
        {
        }
        public async Task<Result> FindByCode(string code)
        {
            return await this._dbSet.Where(x => x.Code == code).FirstOrDefaultAsync();
        }
    }
}