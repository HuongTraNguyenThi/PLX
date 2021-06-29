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
using PLX.API.Data.DTO;
using PLX.API.Data.Models;
using PLX.API.Data.Repositories;
using PLX.API.Extensions.Converters;
using PLX.API.Services;

namespace PLX.API.MiddleWare
{
    public class LogRequestResponseMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogRequestResponseMiddleWare> _logger;
        private IRepository<LogAPI> _iLogApiRepository;
        private IUnitOfWork _unitOfWork;
        private IResultMessageService _iResultMessageService;
        private Func<string, Exception, string> _defaultFormatter = (state, exception) => state;

        public LogRequestResponseMiddleWare(RequestDelegate next, ILogger<LogRequestResponseMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IRepository<LogAPI> iLogApiRepository, IUnitOfWork unitOfWork,
        IResultMessageService iResultMessageService)
        {
            _iLogApiRepository = iLogApiRepository;
            _unitOfWork = unitOfWork;
            _iResultMessageService = iResultMessageService;
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;
            var services = context.RequestServices;
            var logRepo = services.GetService(typeof(IRepository<LogAPI>));
            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);
            var url = context.Request.Path;
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            _logger.Log(LogLevel.Information, 1, $"REQUEST METHOD: {context.Request.Method}" +
                                                 $"REQUEST BODY: {requestBodyText}" +

                                                 $"REQUEST URL: {url}", null, _defaultFormatter);

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            var bodyStream = context.Response.Body;
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;


            await _next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();
            _logger.Log(LogLevel.Information, 1, $"RESPONSE LOG: {responseBody}" +
                                                    $"RESPONSE CONTENT TYPE: {context.Response.ContentType} " +
                                                   $"RESPONSE STATUS CODE: {context.Response.StatusCode}", null, _defaultFormatter);


            var responseContenDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);

            var responseData = JsonConvert.SerializeObject(responseContenDic["data"]);
            var responseDataContenDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

            // byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseDataContenDic));
            // responseBodyStream = new MemoryStream(byteArray);

            // await responseBodyStream.CopyToAsync(bodyStream);

            var requestContentDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(requestBodyText);
            var requestId = requestContentDic.GetValueOrDefault("requestId");
            var requestTime = requestContentDic.GetValueOrDefault("requestTime");
            var responseId = responseContenDic.GetValueOrDefault("responseId");

            var responseTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //var responseTimeConvert = DateTimeConvert.ToString(responseTime);

            var responseResult = JsonConvert.SerializeObject(responseContenDic["result"]);
            var responseResultContenDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseResult);

            responseDataContenDic["responseId"] = requestId;
            responseDataContenDic["responseTime"] = responseTime;
            byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseDataContenDic));
            responseBodyStream = new MemoryStream(byteArray);

            await responseBodyStream.CopyToAsync(bodyStream);
            context.Request.Body = originalRequestBody;

            var resultCode = responseResultContenDic.GetValueOrDefault("resultCode");
            var resultMess = await _iResultMessageService.GetMessage(resultCode as string, new object[] { "Name", "Phone" });


            var rs = new LogAPI
            {
                RequestId = requestId.ToString(),
                ContentRequest = requestBodyText,
                ApiName = url,
                RequestTime = DateTimeConvert.ToDateTime(requestTime.ToString()),
                ResponseTime = DateTimeConvert.ToDateTime(responseTime),
                ResultCode = resultCode.ToString(),
                ResultMessage = resultMess
            };
            await _iLogApiRepository.AddAsync(rs);
            await _unitOfWork.CompleteAsync();


        }


    }
}