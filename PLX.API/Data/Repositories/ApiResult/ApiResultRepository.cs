using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Contexts;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
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