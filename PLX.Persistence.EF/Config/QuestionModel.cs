using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class QuestionModel : ModelConfig
    {
        public QuestionModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<Question>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Content).IsRequired();
            });
        }
    }
}