namespace PLX.API.Data.DTO.Customer
{
    public class VehicleDTO : BaseResponse
    {
        public string Name { get; set; }
        public string LicensePlate { get; set; }

        public int VehicleTypeId { get; set; }
    }
}