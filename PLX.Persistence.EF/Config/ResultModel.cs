using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class ResultModel : ModelConfig
    {
        public ResultModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<Result>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Code).IsRequired();
                e.HasIndex(e => e.Code).IsUnique();
                e.Property(e => e.Message).IsRequired();
            });
        }
    }
}