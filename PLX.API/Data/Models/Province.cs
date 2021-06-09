using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    [Table("Province")]
    public class Province : BaseEntity
    {

        [Required]
        [Column("Name")]
        public string Name { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}