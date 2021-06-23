using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class LinkedCardRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CardNumber { get; set; }

    }
}