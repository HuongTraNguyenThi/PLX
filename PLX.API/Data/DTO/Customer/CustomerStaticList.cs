using System.Collections.Generic;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerStaticList : ResultMessageResponse
    {
        public List<ListItem> QuestionsOne { get; set; }
        public List<ListItem> QuestionsTwo { get; set; }
        public List<ListItem> Genders { get; set; }
        public List<ListItem> Provinces { get; set; }
    }
}