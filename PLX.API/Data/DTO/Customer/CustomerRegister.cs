using System;
using System.Collections.Generic;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerRegister : Request
    {
        public CustomerInfo CustomerInfo { get; set; }
        public List<VehicleDTO> Vehicles { get; set; }
        public List<LinkedCardDTO> LinkedCards { get; set; }

    }
}