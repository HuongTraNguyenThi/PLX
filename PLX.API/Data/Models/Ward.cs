using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    [Table("Ward")]
    public class Ward : BaseEntity
    {
        
        [Required]
        [Column("Name")]
        public string Name { get; set; }
        public int DistrictId {get; set;}
        [ForeignKey("DistrictId")]
        public District district {get; set;}
    }
}