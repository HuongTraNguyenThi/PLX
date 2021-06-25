using System;
using System.ComponentModel.DataAnnotations;
using PLX.API.Extensions.Converters;

namespace PLX.API.Data.DTO.Customer
{
    public class CustomerCard
    {

        public string CardId { get; set; }
        [Required]
        public string Date { get; set; }
        public string Gender { get; set; }

        public string TaxCode { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int WardId { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
