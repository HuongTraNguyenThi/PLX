using System;
namespace PLX.Persistence.Model
{
    public class OTP : BaseEntity
    {
        public string Phone { get; set; }
        public string OTPCode { get; set; }
        public DateTime CreateTime11 { get; set; }
        public bool? Active { get; set; }
        public string TransactionType { get; set; }
    }
}