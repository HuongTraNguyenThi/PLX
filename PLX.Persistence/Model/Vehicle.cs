
using System;

namespace PLX.Persistence.Model
{
    public class Vehicle : BaseEntity
    {
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public bool? Active { get; set; }
    }
}