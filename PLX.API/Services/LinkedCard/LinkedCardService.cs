using PLX.API.Data.Models;
using AutoMapper;
using PLX.API.Data.Repositories;
using PLX.API.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Services
{

    public class LinkedCardService
    {
        private readonly IRepository<LinkedCard> _linkedCardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public LinkedCardService(IRepository<LinkedCard> linkedCardRepository, IUnitOfWork unitOfWork,
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
        public async Task<APIResponse> AddAsync(LinkedCardDTO linkedCardDTO)
        {
            var linkedCard = _mapper.Map<LinkedCardDTO, LinkedCard>(linkedCardDTO);
            await _linkedCardRepository.AddAsync(linkedCard);
            await _unitOfWork.CompleteAsync();
            return new APIResponse(linkedCardDTO);
        }
        public async Task<APIResponse> UpdateAsync(int id, LinkedCardDTO linkedCardDTO)
        {
            var linkedCard = _mapper.Map<LinkedCardDTO, LinkedCard>(linkedCardDTO);
            _linkedCardRepository.Update(linkedCard);
            await _unitOfWork.CompleteAsync();
            return new APIResponse(linkedCardDTO);
        }
        public async Task<APIResponse> DeleteAsync(int id)
        {
            var linkedCard = await _linkedCardRepository.FindAsync(id);
            var linkedCardDTO = _mapper.Map<LinkedCard, LinkedCardDTO>(linkedCard);
            _linkedCardRepository.Remove(linkedCard);
            await _unitOfWork.CompleteAsync();
            return new APIResponse(linkedCardDTO);
        }
    }
}