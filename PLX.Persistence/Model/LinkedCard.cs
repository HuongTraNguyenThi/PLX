using System;
using System.Collections.Generic;

namespace PLX.Persistence.Model
{
    public class LinkedCard : BaseEntity
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}