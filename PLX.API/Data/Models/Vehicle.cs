using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    [Table("Vehicle")]
    public class Vehicle : BaseEntity
    {
        [Required]
        [Column("Driver Name")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [Column("License Plate")]
        [MaxLength(11)]
        public string LicensePlate { get; set; }
      
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleType {get; set; }
    }
}