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
        [Route("staticlist")]
        public async Task<IActionResult> GetCustomerStaticList(BaseRequest baseRequest)
        {
            var response = await _customerService.GetStaticLists();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(DistrictDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("districtlist/{provinceId?}")]
        public async Task<IActionResult> GetDistrictList(int provinceId, BaseRequest baseRequest)
        {
            var response = await _customerService.GetDistrictsByProvinceId(provinceId);
            if (response.Result.Success)
                return Ok(response);
            return NotFound(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(WardDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("wardlist/{districtId?}")]
        public async Task<IActionResult> GetWardList(int districtId, BaseRequest baseRequest)
        {

            var response = await _customerService.GetWardsByDistrictId(districtId);
            if (!response.Result.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetCustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("getcustomer/{id?}")]
        public async Task<IActionResult> GetCustomerById(BaseRequest baseRequest, int id)
        {
            var response = await _customerService.GetCustomerById(baseRequest, id);
            if (!response.Result.Success)
                return NotFound(response);
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
