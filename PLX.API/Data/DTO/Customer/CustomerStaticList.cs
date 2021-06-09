using System.Collections.Generic;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerStaticList
    {
        public List<ListItem> Questions { get; set; }
        public List<ListItem> Genders { get; set; }
        public List<ListItem> Provinces { get; set; }
    }
}