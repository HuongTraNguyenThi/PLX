using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class DistrictModel : ModelConfig
    {
        public DistrictModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<District>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).IsRequired();
                e.HasOne(d => d.Province);
                e.HasMany(d => d.Wards).WithOne(w => w.District);
            });
        }
    }
}