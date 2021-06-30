using System;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class AuthenticationRequest : BaseRequest
    {

        public string Phone { get; set; }

        public string Password { get; set; }
    }
}