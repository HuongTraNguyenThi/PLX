using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Npgsql;
using PLX.API.Constants;
using PLX.API.Data.DTO;
using PLX.API.Extensions;

namespace PLX.API.MiddleWare
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            try
            {
                await _next(context);
            }
            catch (PostgresException pe)
            {
                var error = new ApiErrorResponse(ResultCodeConstants.ConnectionString, null);

                await response.WriteAsync(error.ToJson());

            }
            catch (AutoMapperMappingException ex)
            {
                var error = new ApiErrorResponse(ResultCodeConstants.EInternalServerError, null);
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await response.WriteAsync(error.ToJson());
            }
            catch (Exception error)
            {
                switch (error)
                {
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                _logger.LogError(error, "--- Fail to process {0}", context.Request.Path);
                var errorResponse = new ApiErrorResponse(ResultCodeConstants.EInternalServerError, null);
                await response.WriteAsync(errorResponse.ToJson());
            }
        }
    }
}
