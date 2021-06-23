using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Models;

namespace PLX.API.Data.Repositories
{
    public static class ResultMessageReponsitoryExtension
    {
        public static async Task<Result> FindFormatMessageByCode(this IRepository<Result> resultRepository, string code)
        {
            var format = await resultRepository.Entities.Where(x => x.Code == code).FirstOrDefaultAsync();
            return format;
        }

    }
}
