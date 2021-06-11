using System;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class AuthenticationRequest
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}