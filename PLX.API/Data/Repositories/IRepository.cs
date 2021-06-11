using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        //T:generic type
        public DbSet<T> Entities { get; }
        Task<List<T>> ListAsync();
        Task<T> FindAsync(params object[] keyValues);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);

        void Update(T entity);
        void Remove(T entity);
    }
}