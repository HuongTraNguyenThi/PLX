using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class LinkedCardRequest
    {
        public string Name { get; set; }

        public string CardNumber { get; set; }

    }
}