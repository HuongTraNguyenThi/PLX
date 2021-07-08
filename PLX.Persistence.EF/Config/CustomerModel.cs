using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class CustomerModel : ModelConfig
    {
        public CustomerModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).IsRequired().HasMaxLength(200);
                e.Property(e => e.Phone).IsRequired().HasMaxLength(12);
                e.Property(e => e.Email).HasMaxLength(100);
                e.Property(e => e.Password).IsRequired().HasMaxLength(200);
                e.Property(e => e.CardID).HasMaxLength(12);
                e.Property(e => e.Date).IsRequired().HasColumnType("date");
                e.Property(e => e.Active).HasDefaultValue(true);
                e.Property(e => e.Address).IsRequired();

                e.HasOne(e => e.CustomerType);
                e.HasOne(e => e.Province);
                e.HasOne(e => e.District);
                e.HasOne(e => e.Ward);

                e.HasMany(e => e.LinkedCards).WithOne(e => e.Customer);
                e.HasMany(e => e.Vehicles).WithOne(e => e.Customer);
                e.HasMany(e => e.Questions).WithOne(e => e.Customer);
            });
        }
    }
}