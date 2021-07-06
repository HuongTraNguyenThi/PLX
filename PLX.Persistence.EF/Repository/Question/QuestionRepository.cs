using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.EF.Context;
using PLX.Persistence.Model;
using PLX.Persistence.Repository;

namespace PLX.Persistence.EF.Repository
{
    public class QuestionRepository : AbstractRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(PLXDbContext context) : base(context)
        {
        }

        public async Task<List<Question>> ListQuestionOne()
        {
            return await this._dbSet.OrderBy(q => q.Id).Take(5).ToListAsync();
        }

        public async Task<List<Question>> ListQuestionTwo()
        {
            return await this._dbSet.OrderByDescending(q => q.Id).Take(5).ToListAsync();
        }
    }
}