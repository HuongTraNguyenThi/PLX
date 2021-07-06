using System;
using Microsoft.EntityFrameworkCore;

namespace PLX.Persistence.EF.Config
{
    public abstract class ModelConfig
    {
        protected ModelBuilder modelBuilder;
        public ModelConfig(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public abstract void SetupModel();
    }
}