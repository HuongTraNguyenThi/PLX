using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticationRequest authRequest)
        {
            var response = await _authenticationService.Authenticate(authRequest);
            if (response.Success)
            {
                return Ok(response.Resource);
            }
            return BadRequest(response.ErrorMessage);
        }
        [AllowAnonymous]
        [HttpPost("genarateotp")]
        public async Task<IActionResult> GenerateOtp(OTPDTO oTPDTO)
        {
            var response = await _authenticationService.GenerateOTP(oTPDTO);
            if (response.Success)
            {
                return Ok(response.Resource);
            }
            return BadRequest(response.ErrorMessage);
        }
    }
}