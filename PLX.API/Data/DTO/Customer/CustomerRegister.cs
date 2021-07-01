using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerRegister : BaseRequest
    {

        public CustomerInfo CustomerInfo { get; set; }
        public List<VehicleDTO> Vehicles { get; set; }
        public List<LinkedCardDTO> LinkedCards { get; set; }
    }
}