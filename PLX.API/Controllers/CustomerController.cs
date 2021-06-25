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
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("register")]
        public async Task<IActionResult> Register(CustomerRegister dto)
        {
            var response = await _iCustomerService.RegisterAsync(dto);
            if (response.Result.Success)
                return Ok(response);

            return BadRequest(response);
        }
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerStaticList), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("staticlist")]
        public async Task<IActionResult> GetCustomerStaticList(BaseRequest baseRequest)
        {
            var response = await _iCustomerService.GetLists(baseRequest);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(DistrictDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("districtlist/{id?}")]
        public async Task<IActionResult> GetDistrictList(BaseRequest baseRequest, int id)
        {
            var response = await _iCustomerService.GetListDistricts(baseRequest, id);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(WardDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("wardlist/{id?}")]
        public async Task<IActionResult> GetWardList(BaseRequest baseRequest, int id)
        {

            var response = await _iCustomerService.GetListWards(baseRequest, id);
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("getcustomer/{id?}")]
        public async Task<IActionResult> GetCustomerById(BaseRequest baseRequest, int id)
        {
            var response = await _iCustomerService.GetCustomerById(baseRequest, id);
            return Ok(response);
        }
        ///////
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(CustomerStaticList), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("staticlist")]
        public async Task<IActionResult> GetStaticList()
        {
            var response = await _iCustomerService.GetLists();
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(DistrictDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("districtlist/{id?}")]
        public async Task<IActionResult> GetDistrict(int id)
        {
            var response = await _iCustomerService.GetListDistricts(id);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(WardDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("wardlist/{id?}")]
        public async Task<IActionResult> GetWard(int id)
        {

            var response = await _iCustomerService.GetListWards(id);
            return Ok(response);
        }


    }
}
