using System;
using System.Collections.Generic;

namespace PLX.Persistence.Model
{
    public class Province : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}