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
    public class VehicleRepository : AbstractRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(PLXDbContext context) : base(context)
        {
        }

        public async Task<List<Vehicle>> FindByIdCustomer(int id)
        {
            var query = this._dbSet.Where(x => x.CustomerId == id);
            return await query.ToListAsync();
        }
    }
}