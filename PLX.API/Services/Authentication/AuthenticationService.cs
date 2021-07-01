using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PLX.API.Data.Models;
using PLX.API.Data.Repositories;
using PLX.API.Data.DTO;
using PLX.API.Helpers;
using PLX.API.Data.DTO.Customer;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using System;
using System.Text.RegularExpressions;
using PLX.API.Constants;

namespace PLX.API.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private JwtConfig _jwtConfig;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<OTP> _otpRepository;
        private readonly IRepository<CustomerLog> _customerLogRespository;
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationService(ILogger<AuthenticationService> logger, IOptions<JwtConfig> options, IRepository<Customer> customerRepository,
        IMapper mapper, IRepository<OTP> otpRepository, IUnitOfWork unitOfWork, IRepository<CustomerLog> customerLogRespository)
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
                return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Số điện thoại" });

            var isValidPhone = Validation.IsValidPhone(authRequest.Phone);
            if (!isValidPhone)
                return ErrorResponse(ResultCodeConstants.ErrorInvalidPhone);

            var customer = await _customerRepository.FindCustomerByPhoneAndPasword(authRequest.Phone);
            if (customer == null || !BC.Verify(authRequest.Password, customer.Password))
                return ErrorResponse(ResultCodeConstants.ErrorAuthenticate);

            IDictionary<string, object> customerInfo = new Dictionary<string, object>();
            customerInfo.Add("Id", customer.Id.ToString());
            customerInfo.Add("Phone", customer.Phone);
            string token = JwtHelper.GenerateToken(_jwtConfig, customerInfo);
            var customerDto = _mapper.Map<Customer, CustomerDTO>(customer);

            AuthenticationResponse authResponse = new AuthenticationResponse()
            {
                Token = token,
                Customer = customerDto
            };

            var customerLog = new CustomerLog()
            {
                CustomerId = customer.CustomerTypeId,
                Time = DateTime.Now
            };
            var response = OkResponse(authResponse, ResultCodeConstants.SuccessAuthenticate);
            await _customerLogRespository.AddAsync(customerLog);
            return response;
        }
        public async Task<APIResponse> FindUserById(int id)
        {
            var customer = await _customerRepository.FindAsync(id);
            var cusDTO = _mapper.Map<Customer, CustomerDTO>(customer);
            return OkResponse(cusDTO);
        }

        public async Task<APIResponse> GenerateOTP(OTPGenerateRequest oTPRequest)
        {
            if (oTPRequest.Phone == null || oTPRequest.Phone == "")
                return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Số điện thoại" });

            var CheckPhone = Validation.IsValidPhone(oTPRequest.Phone);
            if (!CheckPhone)
                return ErrorResponse(ResultCodeConstants.ErrorInvalidPhone);

            string otp = new Random().Next(100000, 999999).ToString();
            var otpRecord = await _otpRepository.FindOTPByPhoneAndActive(oTPRequest.Phone);
            foreach (var item in otpRecord)
            {
                item.Active = false;
            }
            var result = new OTP()
            {
                Phone = oTPRequest.Phone,
                OTPCode = otp,
                CreateTime11 = DateTime.Now

            };
            await _otpRepository.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            return OkResponse(new OTPResponse("Mã OTP đã được gửi"), ResultCodeConstants.Success);
        }

        public APIResponse ValidateOTP(OTPValidateRequest oTPRequest)
        {
            var otp = "123456";
            if (oTPRequest.Phone == null || oTPRequest.Phone == "")
                return ErrorResponse(ResultCodeConstants.ErrorRegister, new object[] { "Số điện thoại" });

            var CheckPhone = Validation.IsValidPhone(oTPRequest.Phone);
            if (!CheckPhone)
                return ErrorResponse(ResultCodeConstants.ErrorInvalidPhone);

            if (otp != oTPRequest.OtpCode)
                return ErrorResponse(ResultCodeConstants.ErrorInvalidOtp);

            return OkResponse(new OTPResponse("Xác thực thành công"), ResultCodeConstants.SuccessValidOtp);
        }

    }
}
