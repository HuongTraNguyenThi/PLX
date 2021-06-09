using PLX.API.Data.Models;
using AutoMapper;
using PLX.API.Data.Repositories;
using PLX.API.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PLX.API.Services
{
    
    public class LinkedCardService 
    {
       private readonly IRepository<LinkedCard> _linkedCardRepository;
        private readonly IUnitOfWork _unitOfWork;
         private IMapper _mapper;
        public LinkedCardService(IRepository<LinkedCard> linkedCardRepository, IUnitOfWork unitOfWork,
        IMapper mapper) {
            _linkedCardRepository = linkedCardRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<LinkedCard>> ListAsync()
        {
            var linkedCardList = await _linkedCardRepository.ListAsync();
            return linkedCardList;
        }
        public async Task<LinkedCard> FindById(int id) {
            var linkedCard = await _linkedCardRepository.FindAsync(id);
            return linkedCard;
        }
        public async Task<BaseResponse<LinkedCard>> AddAsync(LinkedCard linkedCard)
        {
            await _linkedCardRepository.AddAsync(linkedCard);
            await _unitOfWork.CompleteAsync();
            return new BaseResponse<LinkedCard>(linkedCard);
        }
        public async Task<BaseResponse<LinkedCard>> UpdateAsync(int id, LinkedCard linkedCard)
        {
            _linkedCardRepository.Update(linkedCard);
            await _unitOfWork.CompleteAsync();
            return new BaseResponse<LinkedCard>(linkedCard);
        }
        public async Task<BaseResponse<LinkedCard>> DeleteAsync(int id)
        {
            var linkedCard = await _linkedCardRepository.FindAsync(id);
            _linkedCardRepository.Remove(linkedCard);
            await _unitOfWork.CompleteAsync();
            return new BaseResponse<LinkedCard>(linkedCard);
        }
    }
}