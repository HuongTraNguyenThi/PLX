

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLX.API.Data.Models
{
    public class OTP : BaseEntity
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string OTPCode { get; set; }
        [Column("CreateTime")]
        public DateTime CreateTime11 { get; set; }
        public bool? Active { get; set; }
        public string TransactionType { get; set; }
    }
}