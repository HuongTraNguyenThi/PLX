using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class VehicleRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]

        public int VehicleTypeId { get; set; }
    }
}