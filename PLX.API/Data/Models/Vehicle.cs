using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    [Table("Vehicle")]
    public class Vehicle : BaseEntity
    {
        [Required]
        [Column("DriverName")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [Column("LicensePlate")]
        [MaxLength(11)]
        public string LicensePlate { get; set; }
        [Required]
        public int VehicleTypeId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleType { get; set; }
    }
}