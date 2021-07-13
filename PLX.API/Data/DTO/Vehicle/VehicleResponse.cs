using System;
using PLX.API.Constants;

namespace PLX.API.Data.DTO
{
    public class VehicleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public int VehicleTypeId { get; set; }
        public int RecordType { get; set; } = RecordTypes.ExistRecord;
    }
}