using HourlyRate.Core.Contracts;
using HourlyRate.Core.Data.Common;
using HourlyRate.Core.Services;
using WebShopDemo.Core.Data.Common;

namespace HourlyRate.Extensions
{
    public static class HourlyRateServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
