using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class CustomerLogModel : ModelConfig
    {
        public CustomerLogModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<CustomerLog>(e =>
            {
                e.Ignore(e => e.Id).HasKey(e => new { e.CustomerId, e.Time });
                e.HasOne(e => e.Customer);
                e.Property(e => e.Time).HasColumnType("timestamp");
            });
        }
    }
}