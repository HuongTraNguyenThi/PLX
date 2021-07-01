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
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<CustomerService> _logger;
        private IAuthenticationService _authenticationService;
        public AuthenticationController(ILogger<CustomerService> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status401Unauthorized)]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticationRequest authRequest)
        {
            var response = await _authenticationService.Authenticate(authRequest);
            if (response.Result.Success)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }
        [AllowAnonymous]
        [ProducesResponseType(typeof(OTPResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("generateotp")]
        public async Task<IActionResult> GenerateOtp(OTPGenerateRequest oTPRequest)
        {
            var response = await _authenticationService.GenerateOTP(oTPRequest);
            if (response.Result.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [AllowAnonymous]
        [ProducesResponseType(typeof(OTPResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("validateotp")]
        public IActionResult ValidateOtp(OTPValidateRequest oTPRequest)
        {
            var response = _authenticationService.ValidateOTP(oTPRequest);
            if (response.Result.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
