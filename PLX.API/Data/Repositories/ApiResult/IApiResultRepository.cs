using System;
using System.Threading.Tasks;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public interface IApiResultRepository : IBaseRepository<Result>
    {
        Task<Result> FindByCode(string code);
    }
}