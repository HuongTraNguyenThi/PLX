using System;
using Microsoft.Extensions.DependencyInjection;
using PLX.Persistence.EF.Repository;
using PLX.Persistence.Repository;

namespace PLX.API.Data.Repositories
{
    public static class RepositoryScopes
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IApiResultRepository, ApiResultRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerLogRepository, CustomerLogRepository>();
            services.AddScoped<ICustomerQuestionRepository, CustomerQuestionRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<ILinkedCardRepository, LinkedCardRepository>();
            services.AddScoped<ILogAPIRepository, LogAPIRepository>();
            services.AddScoped<IOTPRepository, OTPRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddScoped<IWardRepository, WardRepository>();
            return services;
        }
    }
}