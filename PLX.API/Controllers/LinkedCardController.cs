
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLX.API.Data.DTO;
using PLX.API.Services;

namespace PLX.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class LinkedCardController : ControllerBase
    {
        private readonly ILogger<LinkedCardController> _logger;
        private ILinkedCardService _iLinkedCardService;

        public LinkedCardController(ILogger<LinkedCardController> logger, ILinkedCardService iLinkedCardServicevice)
        {
            _logger = logger;
            _iLinkedCardService = iLinkedCardServicevice;

        }

        [HttpPost]
        [ProducesResponseType(typeof(LinkedCardListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [Route("getlinkedcardsbycustomer/{customerId?}")]
        public async Task<IActionResult> GetLinkedCardList(BaseRequest baseRequest, int customerId)
        {
            var response = await _iLinkedCardService.GetListByIdCustomer(baseRequest, customerId);
            if (response.Result.Success)
                return Ok(response);
            return NotFound(response);
        }
    }
}


