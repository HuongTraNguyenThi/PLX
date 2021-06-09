using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PLX.API.Data.Models
{
    [Table("Question")]
    public class Question : BaseEntity
    {
        public Question()
        {
            this.Customers = new HashSet<CustomerQuestion>();
        }
        [Required]
        [Column("Question")]

        public string Content { get; set; }
        public virtual ICollection<CustomerQuestion> Customers { get; set; }
    }
}