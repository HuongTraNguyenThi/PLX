using System;

namespace PLX.Persistence.Model
{
    public class CustomerLog : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Time { get; set; }
    }
}