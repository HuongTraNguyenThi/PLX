using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class LinkedCardModel : ModelConfig
    {
        public LinkedCardModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<LinkedCard>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired().HasMaxLength(200);
                e.Property(c => c.CardNumber).IsRequired();
                e.Property(c => c.Active).HasDefaultValue(true);
                e.HasOne(c => c.Customer);
            });
        }
    }
}