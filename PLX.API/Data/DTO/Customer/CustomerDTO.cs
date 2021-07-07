using System;
using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerDTO : ResultMessageResponse
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardId { get; set; }

        public string Date { get; set; }
        public string Gender { get; set; }
        public string TaxCode { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string Address { get; set; }
    }
}
