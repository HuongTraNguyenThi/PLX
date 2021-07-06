using System;
using System.Collections.Generic;

namespace PLX.Persistence.Model
{
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public ICollection<Ward> Wards { get; set; }
    }
}