using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    [Table("VehicleType")]
    public class VehicleType : BaseEntity
    {
        
        [Required]
        [Column("Name")]
        public string Name { get; set; }
        public virtual ICollection<Vehicle> Vehicles {get; set;}
    }
}