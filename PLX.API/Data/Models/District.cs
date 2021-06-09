using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    [Table("District")]
    public class District : BaseEntity
    {

        [Required]
        [Column("Name")]
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}