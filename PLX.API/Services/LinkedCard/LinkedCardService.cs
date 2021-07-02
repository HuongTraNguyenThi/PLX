using PLX.API.Data.Models;
using AutoMapper;
using PLX.API.Data.Repositories;
using PLX.API.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Services
{

    public class LinkedCardService : ILinkedCardService
    {
        private readonly ILinkedCardRepository _linkedCardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public LinkedCardService(ILinkedCardRepository linkedCardRepository, IUnitOfWork unitOfWork,
        IMapper mapper)
        {
            _linkedCardRepository = linkedCardRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<LinkedCard>> ListAsync()
        {
            var linkedCardList = await _linkedCardRepository.ListAsync();
            return linkedCardList;
        }
        public async Task<LinkedCard> FindById(int id)
        {
            var linkedCard = await _linkedCardRepository.FindAsync(id);
            return linkedCard;
        }
    }
}