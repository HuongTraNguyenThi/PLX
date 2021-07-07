using System.Collections.Generic;

namespace PLX.API.Data.DTO.Customer
{
    public class VehicleTypeList : ResultMessageResponse
    {
        public List<ListItem> VehicleTypes { get; set; }
    }
}