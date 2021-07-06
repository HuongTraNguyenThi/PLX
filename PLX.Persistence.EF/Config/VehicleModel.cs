using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class VehicleModel : ModelConfig
    {
        public VehicleModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<Vehicle>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).IsRequired().HasMaxLength(200);
                e.Property(e => e.LicensePlate).IsRequired().HasMaxLength(11);
                e.HasIndex(e => e.LicensePlate).IsUnique();
                e.HasOne(e => e.VehicleType);
                e.HasOne(e => e.Customer);
            });
        }
    }
}