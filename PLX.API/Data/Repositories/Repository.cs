using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Contexts;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly PLXDbContext _context;
        private DbSet<T> _entities;
        public DbSet<T> Entities { get; }
        string errorMessage = string.Empty;


        public Repository(PLXDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
            Entities = _entities;
        }
        public async Task<List<T>> ListAsync()
        {
            return await _entities.ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            return await _entities.FindAsync(keyValues);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }


    }
}