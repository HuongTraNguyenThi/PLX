using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Customer;
using PLX.API.Services;

namespace PLX.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private IVehicleService _iVehicleService;
        public VehicleController(ILogger<VehicleController> logger, IVehicleService iVehicleService)
        {
            _logger = logger;
            _iVehicleService = iVehicleService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(VehicleTypeList), StatusCodes.Status200OK)]
        [Route("getvehicletypelist")]
        public async Task<IActionResult> GetVehicleTypeList(BaseRequest baseRequest)
        {
            var response = await _iVehicleService.GetListVehicleType(baseRequest);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(VehicleListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("getvehiclelistbycustomer/{customerId?}")]
        public async Task<IActionResult> GetVehicleList(BaseRequest baseRequest, int customerId)
        {
            var response = await _iVehicleService.GetVehiclesByCustomer(baseRequest, customerId);
            if (response.Result.Success)
                return Ok(response);
            return NotFound(response);
        }
    }
}


