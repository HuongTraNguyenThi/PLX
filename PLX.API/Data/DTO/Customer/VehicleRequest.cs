using System.ComponentModel.DataAnnotations;

namespace PLX.API.Data.DTO.Customer
{
    public class VehicleRequest
    {

        public string Name { get; set; }

        public string LicensePlate { get; set; }


        public int VehicleTypeId { get; set; }
    }
}