using System;

namespace PLX.API.Data.DTO.Customer
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Token { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}