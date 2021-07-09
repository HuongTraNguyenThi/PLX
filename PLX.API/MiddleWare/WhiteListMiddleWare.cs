using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PLX.API.Constants;
using PLX.API.Data.DTO;
using PLX.API.Extensions;

namespace PLX.API.MiddleWare
{
    public class WhiteListMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<WhiteListMiddleWare> _logger;
        private readonly byte[][] _safelist;

        public WhiteListMiddleWare(RequestDelegate next, ILogger<WhiteListMiddleWare> logger, string safelist)
        {
            var ips = safelist.Split(';');
            _safelist = new byte[ips.Length][];
            for (var i = 0; i < ips.Length; i++)
            {
                _safelist[i] = IPAddress.Parse(ips[i]).GetAddressBytes();
            }

            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethod.Get.Method)
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                _logger.LogDebug("Request from Remote IP address: {RemoteIp}", remoteIp);

                var bytes = remoteIp.GetAddressBytes();
                var badIp = true;

                var response = context.Response;
                response.ContentType = "application/json";

                foreach (var address in _safelist)
                {
                    if (address.SequenceEqual(bytes))
                    {
                        badIp = false;
                        break;
                    }
                }

                if (badIp)
                {
                    _logger.LogWarning(
                        "Forbidden Request from Remote IP address: {RemoteIp}", remoteIp);
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    var errorResponse = new ApiErrorResponse(ResultCodeConstants.Forbidden, null);
                    await response.WriteAsync(errorResponse.ToJson());

                    return;
                }
            }

            await _next.Invoke(context);

        }
    }
}
