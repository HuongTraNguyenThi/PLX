using System.Collections.Generic;

namespace PLX.API.Data.DTO.Customer
{
    public class DistrictDTO : BaseResponse
    {
        public DistrictDTO(List<ListItem> districts)
        {
            Districts = districts;
        }
        public List<ListItem> Districts { get; set; }

    }
}