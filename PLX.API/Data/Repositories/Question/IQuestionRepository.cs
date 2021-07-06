using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> ListQuestionOne();
        Task<List<Question>> ListQuestionTwo();

    }
}