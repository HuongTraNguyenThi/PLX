using System.Collections.Generic;

namespace PLX.API.Data.DTO.Customer
{
    public class WardDTO : BaseResponse
    {
        public WardDTO(List<ListItem> wards)
        {
            Wards = wards;
        }
        public List<ListItem> Wards { get; set; }

    }
}