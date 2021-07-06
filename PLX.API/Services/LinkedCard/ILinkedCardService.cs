using AutoMapper;
using PLX.API.Data.Repositories;
using PLX.API.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.DTO.Customer;
using PLX.Persistence.Model;

namespace PLX.API.Services
{
    public interface ILinkedCardService
    {
        Task<List<LinkedCard>> ListAsync();
        Task<LinkedCard> FindById(int id);
    }
}