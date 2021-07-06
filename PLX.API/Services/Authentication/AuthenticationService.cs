using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PLX.API.Data.Models;
using PLX.API.Data.Repositories;
using PLX.API.Data.DTO;
using PLX.API.Data.DTO.Authentication;
using PLX.API.Helpers;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using PLX.API.Constants;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private JwtConfig _jwtConfig;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOTPRepository _otpRepository;
        private readonly ICustomerLogRepository _customerLogRespository;
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationService(ILogger<AuthenticationService> logger, IOptions<JwtConfig> options, ICustomerRepository customerRepository,
        IMapper mapper, IOTPRepository otpRepository, IUnitOfWork unitOfWork, ICustomerLogRepository customerLogRespository)
        {
            _logger = logger;
            _jwtConfig = options.Value;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _otpRepository = otpRepository;
            _unitOfWork = unitOfWork;
            _customerLogRespository = customerLogRespository;
        }
        public async Task<APIResponse> Authenticate(AuthenticationRequest authRequest)
        {
            if (Validation.IsNullOrEmpty(authRequest.Phone))
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Số điện thoại" });

            if (!Validation.IsValidPhone(authRequest.Phone))
                return ErrorResponse(ResultCodeConstants.EInvalidPhoneFormat);

            var customer = await _customerRepository.FindByPhone(authRequest.Phone);
            if (customer == null || !BC.Verify(authRequest.Password, customer.Password))
                return ErrorResponse(ResultCodeConstants.AuthEWrongUserOrPassword);

            IDictionary<string, object> customerInfo = new Dictionary<string, object>();
            customerInfo.Add("Id", customer.Id.ToString());
            customerInfo.Add("Phone", customer.Phone);
            string token = JwtHelper.GenerateToken(_jwtConfig, customerInfo);

            AuthenticationResponse authResponse = _mapper.Map<Customer, AuthenticationResponse>(customer);
            authResponse.Token = token;

            var customerLog = new CustomerLog()
            {
                CustomerId = customer.CustomerTypeId,
                Time = DateTime.Now
            };
            var response = OkResponse(authResponse, ResultCodeConstants.AuthSuccessLogin);
            // var response = new ApiOkResponse(authResponse, ResultCodeConstants.AuthSuccessLogin);
            await _customerLogRespository.AddAsync(customerLog);
            return response;
        }
        public async Task<APIResponse> FindUserById(int id)
        {
            var customer = await _customerRepository.FindAsync(id);
            var cusDTO = _mapper.Map<Customer, CustomerDTO>(customer);
            return OkResponse(cusDTO, "10001");
        }

        public async Task<APIResponse> GenerateOTP(OTPGenerateRequest otpRequest)
        {
            var error = _validateOTPRequest(otpRequest);
            if (error != null) return error;

            string otp = new Random().Next(100000, 999999).ToString();
            var otpRecord = await _otpRepository.ListByPhone(otpRequest.Phone);
            foreach (var item in otpRecord)
            {
                item.Active = false;
            }
            var result = new OTP()
            {
                Phone = otpRequest.Phone,
                OTPCode = otp,
                CreateTime11 = DateTime.Now,
                TransactionType = otpRequest.TransactionType
            };
            await _otpRepository.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            return OkResponse(new OTPResponse("Mã OTP đã được gửi"), ResultCodeConstants.Success);
        }

        public APIResponse ValidateOTP(OTPValidateRequest oTPRequest)
        {
            var otp = "123456";
            if (oTPRequest.Phone == null || oTPRequest.Phone == "")
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Số điện thoại" });

            if (!Validation.IsValidPhone(oTPRequest.Phone))
                return ErrorResponse(ResultCodeConstants.EInvalidPhoneFormat);

            if (otp != oTPRequest.OtpCode)
                return ErrorResponse(ResultCodeConstants.AuthEInvalidOTP);

            return OkResponse(new OTPResponse("Xác thực thành công"), ResultCodeConstants.AuthValidOTP);
        }

        private ApiErrorResponse _validateOTPRequest(OTPGenerateRequest otpRequest)
        {
            if (Validation.IsNullOrEmpty(otpRequest.TransactionType))
            {
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Loại giao dịch" });
            }

            if (!Validation.Equals(TransactionTypes.Register, otpRequest.TransactionType) &&
                !Validation.Equals(TransactionTypes.Register, otpRequest.TransactionType))
            {
                return ErrorResponse(ResultCodeConstants.AuthUnsupportedOTPType, new object[] { otpRequest.TransactionType });
            }

            if (Validation.IsNullOrEmpty(otpRequest.Phone))
                return ErrorResponse(ResultCodeConstants.ENullOrEmptyValue, new object[] { "Số điện thoại" });

            if (!Validation.IsValidPhone(otpRequest.Phone))
                return ErrorResponse(ResultCodeConstants.EInvalidPhoneFormat);

            return null;
        }
    }
}
