using System;
using Microsoft.EntityFrameworkCore;
using PLX.Persistence.EF.Config;
using PLX.Persistence.Model;

namespace PLX.Persistence.EF.Context
{
    public class PLXDbContext : DbContext
    {
        public PLXDbContext(DbContextOptions<PLXDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RegisterModel<CustomerLogModel>();
            modelBuilder.RegisterModel<CustomerModel>();
            modelBuilder.RegisterModel<CustomerQuestionModel>();
            modelBuilder.RegisterModel<CustomerTypeModel>();
            modelBuilder.RegisterModel<DistrictModel>();
            modelBuilder.RegisterModel<LinkedCardModel>();
            modelBuilder.RegisterModel<LogAPIModel>();
            modelBuilder.RegisterModel<OTPModel>();
            modelBuilder.RegisterModel<ProvinceModel>();
            modelBuilder.RegisterModel<QuestionModel>();
            modelBuilder.RegisterModel<ResultModel>();
            modelBuilder.RegisterModel<VehicleModel>();
            modelBuilder.RegisterModel<VehicleTypeModel>();
            modelBuilder.RegisterModel<WardModel>();
        }
    }

    public static class ModelBuilderExtension
    {
        public static void RegisterModel<T>(this ModelBuilder modelBuilder) where T : ModelConfig
        {
            var modelConfig = Activator.CreateInstance(typeof(T), modelBuilder) as ModelConfig;
            modelConfig.SetupModel();
        }
    }
}