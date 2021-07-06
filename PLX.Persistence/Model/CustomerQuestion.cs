using System;

namespace PLX.Persistence.Model
{
    public class CustomerQuestion : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public string Answer { get; set; }
    }
}