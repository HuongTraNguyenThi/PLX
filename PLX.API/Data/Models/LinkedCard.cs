using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PLX.API.Data.Models
{
    [Table("LinkedCard")]
    public class LinkedCard : BaseEntity
    {
        [Required]
        [Column("Name")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [Column("CardNumber")]

        public string CardNumber { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

    }
}