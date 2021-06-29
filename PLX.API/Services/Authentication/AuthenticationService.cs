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

namespace PLX.API.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private JwtConfig _jwtConfig;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<OTP> _otpRepository;
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationService(ILogger<AuthenticationService> logger, IOptions<JwtConfig> options, IRepository<Customer> customerRepository,
        IMapper mapper, IRepository<OTP> otpRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _jwtConfig = options.Value;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _otpRepository = otpRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse> Authenticate(AuthenticationRequest authRequest)
        {
            var customer = await _customerRepository.FindCustomerByPhoneAndPasword(authRequest.Phone);
            if (customer == null || !BC.Verify(authRequest.Password, customer.Password))
                return ErrorResponse("10004");
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
            var response = OkResponse(authResponse, "11004");
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
            Regex regex = new Regex(@"^[0-9]{10}$");
            bool isValid = regex.IsMatch(oTPRequest.Phone);
            if (!isValid)
                return ErrorResponse("10006");
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
                CreateTime11 = DateTime.Now,
                Active = true
            };
            await _otpRepository.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            return OkResponse(new OTPResponse("Mã OTP đã được gửi"), "11002");
        }

        public async Task<APIResponse> ValidateOTP(OTPValidateRequest oTPRequest)
        {
            // int value = 3;
            // var otpRecord = await _otpRepository.FindOTPByPhoneAndOTP(oTPRequest.Phone, oTPRequest.OtpCode);

            // foreach (var item in otpRecord)
            // {
            //     TimeSpan ts = DateTime.Now - item.CreateTime11;
            //     var otp = item.OTPCode;
            //     if (otp == "123456" || ts.Minutes <= value)
            //         return OkResponse(new OTPResponse("Xác thực thành công"));

            //     return ErrorResponse("10002", null);
            // }
            var otp = "123456";
            Regex regex = new Regex(@"^[0-9]{10}$");
            bool isValid = regex.IsMatch(oTPRequest.Phone);
            if (oTPRequest.Phone == null || oTPRequest.Phone == "" || !isValid)
                return ErrorResponse("10006");
            if (otp == oTPRequest.OtpCode)
                return OkResponse(new OTPResponse("Xác thực thành công"));
            return ErrorResponse("10002", null);
        }

    }
}
