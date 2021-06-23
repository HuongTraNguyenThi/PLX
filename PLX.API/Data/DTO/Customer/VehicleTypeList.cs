using System.Collections.Generic;

namespace PLX.API.Data.DTO.Customer
{
    public class VehicleTypeList : BaseResponse
    {
        public List<ListItem> VehicleTypes { get; set; }
    }
}