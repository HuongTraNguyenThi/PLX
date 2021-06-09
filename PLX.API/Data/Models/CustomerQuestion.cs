using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    public class CustomerQuestion : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        [Column("Answer")]
        public string Answer { get; set; }
    }
}