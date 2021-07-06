using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.Persistence.Model;

namespace PLX.Persistence.Repository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> ListQuestionOne();
        Task<List<Question>> ListQuestionTwo();

    }
}