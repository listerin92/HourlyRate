using HourlyRate.Core.Contracts;
using HourlyRate.Core.Services;
using HourlyRate.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Identity;

namespace HourlyRate.Extensions
{
    public static class HourlyRateServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IGeneralCostService, GeneralCostService>();
            services.AddScoped<ICostCenterService, CostCenterService> ();
            
            return services;
        }
    }
}
