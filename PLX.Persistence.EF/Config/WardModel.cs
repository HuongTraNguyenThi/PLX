using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class WardModel : ModelConfig
    {
        public WardModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<Ward>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).IsRequired();
                e.HasOne(e => e.District);
            });
        }
    }
}