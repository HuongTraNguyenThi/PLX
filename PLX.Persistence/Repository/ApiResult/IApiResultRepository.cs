using System;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface IApiResultRepository : IRepository<Result>
    {
        Task<Result> FindByCode(string code);
    }
}