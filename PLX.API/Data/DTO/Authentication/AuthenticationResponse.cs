using System;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Data.DTO.Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Token { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}