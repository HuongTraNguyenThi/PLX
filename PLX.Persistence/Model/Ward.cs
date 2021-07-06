
using System;

namespace PLX.Persistence.Model
{
    public class Ward : BaseEntity
    {
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}