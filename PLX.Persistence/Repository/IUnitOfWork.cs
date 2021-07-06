using System;
using System.Threading.Tasks;

namespace PLX.Persistence.Repository
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}