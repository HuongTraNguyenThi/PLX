using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PLX.API.Data.DTO;
using PLX.API.Data.Models;
using PLX.API.Data.Repositories;
using PLX.API.Extensions;
using PLX.API.Extensions.Converters;
using PLX.API.Services;

namespace PLX.API.MiddleWare
{
    public class LogRequestResponseMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogRequestResponseMiddleWare> _logger;
        public LogRequestResponseMiddleWare(RequestDelegate next, ILogger<LogRequestResponseMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Parse request body to check if api-call
            var requestBodyStream = new MemoryStream();
            var originRequestBodyStream = context.Request.Body;
            await originRequestBodyStream.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            var baseRequest = JsonConvert.DeserializeObject<BaseRequest>(requestBodyText);
            if (baseRequest == null)
            {
                await _next(context);
                return;
            }

            var originResponseBodyStream = context.Response.Body;
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBodyText = new StreamReader(responseBodyStream).ReadToEnd();

            // Request information
            var requestId = baseRequest.RequestId;
            var requestTime = DateTimeConvert.ToDateTime(baseRequest.RequestTime);
            var requestUri = context.Request.Path;

            // Get app services
            var services = context.RequestServices;
            var unitOfWork = services.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            var logApiRepository = services.GetService(typeof(IRepository<LogAPI>)) as IRepository<LogAPI>;
            var resultMessageService = services.GetService(typeof(IResultMessageService)) as IResultMessageService;

            // Response information
            var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseBodyText);
            var apiResult = apiResponse.Result;
            var dataType = Type.GetType(apiResult.DataType);
            var apiResponseDataJObject = apiResponse.Data as JObject;
            var apiResponseData = apiResponseDataJObject.ToObject(dataType) as BaseResponse;
            var resultCode = apiResult.ResultCode;
            var resultArgs = apiResult.Arguments;
            var resultMessage = await resultMessageService.GetMessage(resultCode as string, resultArgs);
            var responseDateTime = DateTime.Now;
            var responseTimeStr = DateTimeConvert.ToString(responseDateTime);

            // Update response data
            apiResponseData.ResponseId = requestId;
            apiResponseData.ResponseTime = responseTimeStr;

            // Update error message
            if (!apiResult.Success)
            {
                var errorMessageData = apiResponseData as ErrorMessageResponse;
                errorMessageData.ErrorMessage = resultMessage;
            }

            // Send final response data
            byte[] byteArray = Encoding.UTF8.GetBytes(apiResponseData.ToJson());
            responseBodyStream = new MemoryStream(byteArray);
            await responseBodyStream.CopyToAsync(originResponseBodyStream);

            var logApi = new LogAPI
            {
                RequestId = requestId,
                RequestTime = requestTime,
                ApiName = requestUri,
                ContentRequest = requestBodyText,
                ResultCode = resultCode,
                ResultMessage = resultMessage,
                ResponseTime = responseDateTime
            };

            await logApiRepository.AddAsync(logApi);
            await unitOfWork.CompleteAsync();
        }
    }
}
