using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PLX.API.Data.Models;
using PLX.API.Data.Repositories;
using PLX.API.Helpers;
using AutoMapper;

using System;

namespace PLX.API.Services
{
    public class ResutlMessageService : IResultMessageService
    {
        private readonly ILogger<ResutlMessageService> _logger;
        private readonly IRepository<Result> _resultRepository;
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ResutlMessageService(ILogger<ResutlMessageService> logger, IRepository<Result> resultRepository,
        IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _resultRepository = resultRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetMessage(string resultCode, params object[] arguments)
        {
            var resultRecord = await _resultRepository.FindFormatMessageByCode(resultCode);
            var args = arguments ??= new object[] { };
            string format = resultRecord.Message;
            string resultMessage = string.Format(format, arguments);
            return resultMessage;
        }
    }
}
