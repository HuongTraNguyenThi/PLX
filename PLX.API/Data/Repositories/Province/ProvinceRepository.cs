using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Contexts;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public class ProvinceRepository : AbstractRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(PLXDbContext context) : base(context)
        {
        }
    }
}