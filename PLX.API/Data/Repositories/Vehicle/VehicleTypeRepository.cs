using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Contexts;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public class VehicleTypeRepository : AbstractRepository<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(PLXDbContext context) : base(context)
        {
        }
    }
}