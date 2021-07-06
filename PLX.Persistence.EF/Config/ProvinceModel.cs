using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class ProvinceModel : ModelConfig
    {
        public ProvinceModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<Province>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).IsRequired();
                e.HasMany(e => e.Districts).WithOne(p => p.Province);
            });
        }
    }
}