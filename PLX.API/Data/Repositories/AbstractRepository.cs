using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Contexts;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public abstract class AbstractRepository<T> where T : BaseEntity
    {
        private readonly PLXDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public AbstractRepository(PLXDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}