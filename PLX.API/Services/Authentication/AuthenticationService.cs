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

namespace PLX.API.Services
{
    public class AuthenticationService : IAuthenticationService
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
                return new APIResponse(1, "Sai thông tin đăng nhập");
            IDictionary<string, object> customerInfo = new Dictionary<string, object>();
            customerInfo.Add("Id", customer.Id.ToString());
            customerInfo.Add("Phone", customer.Phone);
            string token = JwtHelper.GenerateToken(_jwtConfig, customerInfo);

            AuthenticationResponse authResponse = new AuthenticationResponse() { Token = token };
            var response = new APIResponse(authResponse);
            return response;
        }
        public async Task<APIResponse> FindUserById(int id)
        {
            var customer = await _customerRepository.FindAsync(id);
            var userDTO = _mapper.Map<Customer, CustomerDTO>(customer);
            return new APIResponse(userDTO);
        }

        public async Task<APIResponse> GenerateOTP(OTPDTO oTPDTO)
        {
            int otp = new Random().Next(100000, 999999);
            var otpRecord = await _otpRepository.FindOTPByPhoneAndActive(oTPDTO.Phone, oTPDTO.Active);
            foreach (var item in otpRecord)
            {
                item.Active = false;
            }
            var result = new OTP()
            {
                Phone = oTPDTO.Phone,
                OTPCode = otp,
                CreateTime11 = DateTime.Now,
                Active = true
            };
            await _otpRepository.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            return new APIResponse(new OTPResponse("Mã OTP đã được gửi!"));
        }

        public async Task<APIResponse> ValidateOTP(OTPDTO oTPDTO)
        {
            int value = 30;
            var otpRecord = await _otpRepository.FindOTPByPhoneAndOTP(oTPDTO.Phone, oTPDTO.OtpCode);
            foreach (var item in otpRecord)
            {
                TimeSpan ts = DateTime.Now - item.CreateTime11;
                if (ts.Seconds <= value)
                    return new APIResponse(new OTPResponse("Xác thực thành công"));

                return new APIResponse(new OTPResponse("Mã OTP không đúng"));
            }
            return new APIResponse(new OTPResponse("Sai thông tin"));

        }

    }
}