using AutoMapper;
using PLX.API.Data.Repositories;
using PLX.API.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLX.API.Data.DTO.Customer;
using PLX.Persistence.Repository;
using PLX.Persistence.Model;
using PLX.API.Constants;

namespace PLX.API.Services
{

    public class VehicleService : BaseService, IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleTypeRepository _vehicleTypeReponsitry;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public VehicleService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork,
        IMapper mapper, IVehicleTypeRepository vehicleTypeReponsitry)
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
            return OkResponse(list, ResultCodeConstants.Success);
        }

    }
}
