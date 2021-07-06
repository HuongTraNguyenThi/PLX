using System;
using System.Collections.Generic;
using PLX.API.Data.DTO.Customer;

namespace PLX.API.Data.DTO.Authentication
{
    public class AuthenticationResponse : ResultMessageResponse
    {
        public string Token { get; set; }
        public CustomerResponse Customer { get; set; }

    }
}