using PLX.API.Data.Models;
using AutoMapper;
using PLX.API.Data.Repositories;
using PLX.API.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PLX.API.Services
{

    public class VehicleService
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public VehicleService(IRepository<Vehicle> vehicleRepository, IUnitOfWork unitOfWork,
        IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<Vehicle>> ListAsync()
        {
            var vehicleList = await _vehicleRepository.ListAsync();
            return vehicleList;
        }
        public async Task<Vehicle> FindById(int id)
        {
            var vehicle = await _vehicleRepository.FindAsync(id);
            return vehicle;
        }
        public async Task<BaseResponse<Vehicle>> AddAsync(Vehicle vehicle)
        {
            await _vehicleRepository.AddAsync(vehicle);
            await _unitOfWork.CompleteAsync();
            return new BaseResponse<Vehicle>(vehicle);
        }
        public async Task<BaseResponse<Vehicle>> UpdateAsync(int id, Vehicle vehicle)
        {
            _vehicleRepository.Update(vehicle);
            await _unitOfWork.CompleteAsync();
            return new BaseResponse<Vehicle>(vehicle);
        }
        public async Task<BaseResponse<Vehicle>> DeleteAsync(int id)
        {
            var vehicle = await _vehicleRepository.FindAsync(id);
            _vehicleRepository.Remove(vehicle);
            await _unitOfWork.CompleteAsync();
            return new BaseResponse<Vehicle>(vehicle);
        }
    }
}