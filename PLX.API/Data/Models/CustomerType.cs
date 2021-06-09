using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    [Table("CustomerType")]
    public class CustomerType : BaseEntity
    {
        
        [Required]
        [Column("Name")]
        public string Name { get; set; }
        public virtual ICollection<Customer> Customers {get; set;}
    }
}