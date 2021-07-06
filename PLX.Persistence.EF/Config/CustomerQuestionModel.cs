using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Config
{
    public class CustomerQuestionModel : ModelConfig
    {
        public CustomerQuestionModel(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void SetupModel()
        {
            this.modelBuilder.Entity<CustomerQuestion>(e =>
            {
                e.Ignore(e => e.Id).HasKey(cq => new { cq.CustomerId, cq.QuestionId });
                e.HasOne(e => e.Customer);
                e.HasOne(e => e.Question);
            });
        }
    }
}