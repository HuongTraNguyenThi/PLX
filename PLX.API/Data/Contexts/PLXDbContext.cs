using System;
using Microsoft.EntityFrameworkCore;
using PLX.API.Data.Models;


namespace PLX.API.Data.Contexts
{
    public class PLXDbContext : DbContext
    {
        public PLXDbContext(DbContextOptions<PLXDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().Property("Active").HasDefaultValue(true);
            modelBuilder.Entity<Vehicle>();
            modelBuilder.Entity<LinkedCard>();
            modelBuilder.Entity<Question>();
            modelBuilder.Entity<CustomerQuestion>()
                .Ignore(cq => cq.Id).HasKey(cq => new { cq.CustomerId, cq.QuestionId });
            modelBuilder.Entity<VehicleType>();
            modelBuilder.Entity<Province>();
            modelBuilder.Entity<District>();
            modelBuilder.Entity<Ward>();
            modelBuilder.Entity<CustomerType>();
            modelBuilder.Entity<LogAPI>();
            modelBuilder.Entity<OTP>().Property("Active").HasDefaultValue(true);
            modelBuilder.Entity<Result>();
            modelBuilder.Entity<CustomerLog>();
        }
    }
}