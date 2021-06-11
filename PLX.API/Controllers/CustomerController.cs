using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLX.API.Data.DTO.Customer;
using PLX.API.Data.Models;
using PLX.API.Services;

namespace PLX.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;

        private ICustomerService _iCustomerService;



        public CustomerController(ILogger<CustomerController> logger, ICustomerService iCustomerService)
        {
            _logger = logger;
            _iCustomerService = iCustomerService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(CustomerRegister dto)
        {
            var response = await _iCustomerService.RegisterAsync(dto);
            if (response.Success)
                return Ok(response.Resource);

            return BadRequest(response.ErrorMessage);
        }
        [HttpGet]
        [Route("staticlist")]
        public async Task<IActionResult> GetCustomerStaticList()
        {
            var response = await _iCustomerService.GetLists();
            return Ok(response);
        }
        [HttpGet]
        [Route("districtlist/{id?}")]
        public async Task<IActionResult> GetDistrictList(int id)
        {
            var response = await _iCustomerService.GetListDistricts(id);
            return Ok(response);
        }
        [HttpGet]
        [Route("wardlist/{id?}")]
        public async Task<IActionResult> GetWardList(int id)
        {
            var response = await _iCustomerService.GetListWards(id);
            return Ok(response);
        }
        [HttpGet]
        [Route("getcustomer/{id?}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var response = await _iCustomerService.GetCustomerById(id);
            return Ok(response);
        }
    }
}
