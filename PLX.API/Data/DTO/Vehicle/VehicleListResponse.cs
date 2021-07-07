using System;
using System.Collections.Generic;

namespace PLX.API.Data.DTO
{
    public class VehicleListResponse : ResultMessageResponse
    {
        public List<VehicleResponse> Vehicles { get; set; }
    }
}