using Microsoft.EntityFrameworkCore;
using Service.Api.Database;
using Service.Api.Service.DeviceManager;
using Service.Api.Service.SystemManager.Application;

namespace Service.Api.Service.SystemManager.Config
{
    public static class SystemManagerConfig
    {
        public static void AddSystemManagerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ServiceDatabaseContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ISystemService, SystemService>();
            services.AddScoped<ISensorDataService, SensorDataService>();
            services.AddScoped<IDashboardService, DashboardService>();
        }
    }
}
