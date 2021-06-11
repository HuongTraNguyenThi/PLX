using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using NpgsqlTypes;

namespace PLX.API.Data.Models
{
    [Table("Customer")]
    public class Customer : BaseEntity
    {

        public Customer()
        {
            this.Questions = new HashSet<CustomerQuestion>();
        }

        [Required]
        [Column("Name")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [Column("Phone")]
        [MaxLength(10)]
        public string Phone { get; set; }

        [Column("Email")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [Column("Password")]
        [MaxLength(200)]
        public string Password { get; set; }

        [Column("CardID")]
        [MaxLength(12)]
        public string CardID { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("Gender")]
        public string Gender { get; set; }

        [Column("TaxCode")]

        public string TaxCode { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        [Required]
        [ForeignKey("ProvinceId")]

        public Province Province { get; set; }

        [Required]
        [ForeignKey("DistrictId")]
        public District District { get; set; }

        [Required]
        [ForeignKey("WardId")]
        public Ward Ward { get; set; }
        [Required]
        [Column("Address")]
        public string Address { get; set; }
        public ICollection<LinkedCard> ListLinkedCards { get; set; }
        public ICollection<Vehicle> ListVehicles { get; set; }
        public virtual ICollection<CustomerQuestion> Questions { get; set; }
        public int CustomerTypeId { get; set; }
        [ForeignKey("CustomerTypeId")]
        public CustomerType CustomerType { get; set; }

    }
}