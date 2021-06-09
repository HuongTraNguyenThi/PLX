using System;
using System.Threading.Tasks;

namespace PLX.API.Data.Repositories {
    public interface IUnitOfWork {
        Task CompleteAsync();
    }
}