using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PLX.API.MiddleWare
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                // var result = JsonSerializer.Serialize(new { message = error?.Message });
                //var apiResponse = JsonConvert.DeserializeObject<APIResponse>(error?.Message);
                var result = JsonSerializer.Serialize(new ApiErrorResponse(ResultCodeConstants.Error, null));
                await response.WriteAsync(result);
            }
        }
    }
}
