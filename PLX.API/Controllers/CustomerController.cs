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

        private ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService iCustomerService)
        {
            _logger = logger;
            _customerService = iCustomerService;

        }
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerRegisterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("register")]
        public async Task<IActionResult> Register(CustomerRegister dto)
        {
            var response = await _customerService.RegisterAsync(dto);
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
            var response = await _customerService.GetLists(baseRequest);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(DistrictDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("districtlist/{id?}")]
        public async Task<IActionResult> GetDistrictList(BaseRequest baseRequest, int id)
        {
            var response = await _customerService.GetListDistricts(baseRequest, id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(WardDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("wardlist/{id?}")]
        public async Task<IActionResult> GetWardList(BaseRequest baseRequest, int id)
        {

            var response = await _customerService.GetListWards(baseRequest, id);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("getcustomer/{id?}")]
        public async Task<IActionResult> GetCustomerById(BaseRequest baseRequest, int id)
        {
            var response = await _customerService.GetCustomerById(baseRequest, id);
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
            var response = await _customerService.GetLists();
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(DistrictDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("districtlist/{id?}")]
        public async Task<IActionResult> GetDistrict(int id)
        {
            var response = await _customerService.GetListDistricts(id);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(WardDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("wardlist/{id?}")]
        public async Task<IActionResult> GetWard(int id)
        {

            var response = await _customerService.GetListWards(id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerUpdateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("customer/update/{customerId?}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, CustomerUpdateRequest customerUpdateRequest)
        {
            var response = await _customerService.UpdateCustomer(customerId, customerUpdateRequest);
            return Ok(response);
        }
    }
}
