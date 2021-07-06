using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class LogAPIModel : ModelConfig
    {
        public LogAPIModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<LogAPI>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.RequestTime).HasColumnType("timestamp");
                e.Property(e => e.ResponseTime).HasColumnType("timestamp");
            });
        }
    }
}