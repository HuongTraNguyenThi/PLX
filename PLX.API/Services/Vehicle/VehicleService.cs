using PLX.API.Data.Models;
using AutoMapper;
using PLX.API.Data.Repositories;
using PLX.API.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Services
{

    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<VehicleType> _vehicleTypeReponsitry;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public VehicleService(IRepository<Vehicle> vehicleRepository, IUnitOfWork unitOfWork,
        IMapper mapper, IRepository<VehicleType> vehicleTypeReponsitry)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vehicleTypeReponsitry = vehicleTypeReponsitry;
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

        public async Task<APIResponse> GetListVehicleType(BaseRequest baseRequest)
        {
            var vehicleTypes = await _vehicleTypeReponsitry.ListAsync();

            var vehicleTypeList = _mapper.Map<List<VehicleType>, List<ListItem>>(vehicleTypes);

            var list = new VehicleTypeList
            {
                VehicleTypes = vehicleTypeList
            };
            return new ApiOkResponse(list, "11002");
        }
        public async Task<APIResponse> GetListVehicleType()
        {
            var vehicleTypes = await _vehicleTypeReponsitry.ListAsync();

            var vehicleTypeList = _mapper.Map<List<VehicleType>, List<ListItem>>(vehicleTypes);

            var list = new VehicleTypeList
            {
                VehicleTypes = vehicleTypeList
            };
            return new ApiOkResponse(list, "11002");
        }
        // public async Task<APIResponse> AddAsync(VehicleDTO vehicleDTO)
        // {
        //     var vehicle = _mapper.Map<VehicleDTO, Vehicle>(vehicleDTO);
        //     await _vehicleRepository.AddAsync(vehicle);
        //     await _unitOfWork.CompleteAsync();
        //     return new APIResponse(vehicleDTO);
        // }
        // public async Task<APIResponse> UpdateAsync(int id, VehicleDTO vehicleDTO)
        // {
        //     var vehicle = _mapper.Map<VehicleDTO, Vehicle>(vehicleDTO);
        //     _vehicleRepository.Update(vehicle);
        //     await _unitOfWork.CompleteAsync();
        //     return new APIResponse(vehicleDTO);
        // }
        // public async Task<APIResponse> DeleteAsync(int id)
        // {
        //     var vehicle = await _vehicleRepository.FindAsync(id);
        //     var vehicleDTO = _mapper.Map<Vehicle, VehicleDTO>(vehicle);
        //     _vehicleRepository.Remove(vehicle);
        //     await _unitOfWork.CompleteAsync();
        //     return new APIResponse(vehicleDTO);
        // }
    }
}