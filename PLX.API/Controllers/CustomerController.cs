using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Authentication;
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
            if (response.Result.Success)
                return Ok(response);

            return BadRequest(response);
        }


        [HttpPost]
        [ProducesResponseType(typeof(ChangePasswordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("customer/changepassword")]

        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var userId = HttpContext.User.FindFirst("Id").Value;
            var userIdConvert = Int32.Parse(userId);
            var response = await _customerService.ChangePassword(userIdConvert, changePasswordRequest);
            if (response.Result.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(GetQuestionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("customer/getcustomerquestions")]

        public async Task<IActionResult> GetCustomerQuestions(GetQuestionsRequest questionsRequest)
        {

            var response = await _customerService.GetCustomerQuestions(questionsRequest);
            if (response.Result.Success)
                return Ok(response);

            return NotFound(response);
        }



        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ValidateAnswerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("customer/validateanswer")]

        public async Task<IActionResult> ValidateAnswer(ValidateAnswerRequest answerRequest)
        {

            var response = await _customerService.ValidateAnswer(answerRequest);
            if (response.Result.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(OTPValidateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [Route("customer/validateotp")]

        public async Task<IActionResult> ValidateOtp(OTPValidateRequest oTPValidate)
        {

            var response = await _customerService.ValidateOtp(oTPValidate);
            if (response.Result.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
