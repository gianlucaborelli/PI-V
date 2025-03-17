using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Api.Database;
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
        }
    }
}
