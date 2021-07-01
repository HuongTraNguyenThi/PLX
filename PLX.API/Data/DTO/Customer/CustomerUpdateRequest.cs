using System;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerUpdateRequest : BaseRequest
    {
        public CustomerDTO Customer { get; set; }
    }
}
