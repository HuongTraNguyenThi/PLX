using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> ListAsync();
        Task<T> FindAsync(params object[] keyValues);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void Update(T entity);
        void Remove(T entity);
    }
}