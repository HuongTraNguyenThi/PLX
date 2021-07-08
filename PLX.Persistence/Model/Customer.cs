using System;
using System.Collections.Generic;

namespace PLX.Persistence.Model
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            this.Questions = new HashSet<CustomerQuestion>();
            this.Vehicles = new List<Vehicle>();
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CardID { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }
        public string TaxCode { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }
        public Ward Ward { get; set; }
        public string Address { get; set; }
        public ICollection<LinkedCard> LinkedCards { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<CustomerQuestion> Questions { get; set; }
        public int CustomerTypeId { get; set; }
        public CustomerType CustomerType { get; set; }
        public bool? Active { get; set; }
    }
}
