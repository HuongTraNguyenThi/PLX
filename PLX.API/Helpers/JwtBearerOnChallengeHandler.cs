using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PLX.API.Data.DTO;

namespace PLX.API.Helpers
{
    public class JwtBearerOnChallengeHandler
    {
        public static Task OnChallenge(JwtBearerChallengeContext context)
        {
            // Skip the default logic.
            context.HandleResponse();

            // Set response headers
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            // Customize payload send to client here.
            var payload = new ErrorMessageResponse(2, "Vui lòng đăng nhập");
            return context.Response.WriteAsync(JsonConvert.SerializeObject(payload));
        }
    }
}
