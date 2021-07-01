using System;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerUpdateResponse : BaseResponse
    {
        public CustomerDTO Customer { get; set; }
    }
}
