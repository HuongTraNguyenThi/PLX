using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class OTPModel : ModelConfig
    {
        public OTPModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<OTP>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Phone).IsRequired();
                e.Property(e => e.OTPCode).IsRequired();
                e.Property(e => e.CreateTime11).HasColumnType("timestamp");
                e.Property(e => e.Active).HasDefaultValue(true);
            });
        }
    }
}