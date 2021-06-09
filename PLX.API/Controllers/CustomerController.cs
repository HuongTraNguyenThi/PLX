using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLX.API.Data.DTO.Customer;
using PLX.API.Data.Models;
using PLX.API.Services;

namespace PLX.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerController : ControllerBase
    {
      
        private readonly ILogger<CustomerController> _logger;

        private  ICustomerService _iCustomerService;
        


        public CustomerController(ILogger<CustomerController> logger, ICustomerService iCustomerService )
        {
            _logger = logger;
            _iCustomerService = iCustomerService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(CustomerRegister dto)
        {
            var response = await _iCustomerService.RegisterAsync(dto);
            return Ok(response);
        }
        [HttpGet]
        [Route("staticlist")]
        public async Task<IActionResult> GetCustomerStaticList ()
        {
            var response = await _iCustomerService.GetLists();
            return Ok(response);
        }
        [HttpGet]
        [Route("districtlist")]
        public async Task<IActionResult> GetDistrictList (int id)
        {
            var response = await _iCustomerService.GetListDistricts(id);
            return Ok(response);
        }
        [HttpGet]
        [Route("wardlist")]
        public async Task<IActionResult> GetWardList (int id)
        {
            var response = await _iCustomerService.GetListWards(id);
            return Ok(response);
        }
    }
}
