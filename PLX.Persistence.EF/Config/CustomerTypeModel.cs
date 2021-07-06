using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class CustomerTypeModel : ModelConfig
    {
        public CustomerTypeModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<CustomerType>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).IsRequired();
            });
        }
    }
}