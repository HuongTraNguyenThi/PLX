using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerUpdateResponse : BaseResponse
    {
        public CustomerUpdates Customer { get; set; }
        public ICollection<VehicleDTO> Vehicles { get; set; }
        public ICollection<LinkedCardDTO> LinkedCards { get; set; }
    }
}
