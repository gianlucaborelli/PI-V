using Microsoft.EntityFrameworkCore;
using Service.Api.Service.Authentication.Models;
using Service.Api.Service.SystemManager.Mappings;
using Service.Api.Service.SystemManager.Models;

namespace Service.Api.Database
{
    public class ServiceDatabaseContext : DbContext
    {
        public ServiceDatabaseContext(DbContextOptions<ServiceDatabaseContext> options) : base(options)
        {
        }
        
        public DbSet<Module> Modules { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<SensorData> SensorDatas { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("AppService");

            modelBuilder.Entity<Module>(new ModuleMap().Configure);
            modelBuilder.Entity<Sensor>(new SensorMap().Configure);
            modelBuilder.Entity<Company>(new CompanyMap().Configure);
            modelBuilder.Entity<SensorData>(new SensorDataMap().Configure);
            modelBuilder.Entity<UserCompany>(new UserCompanyMap().Configure);
        }
    }
}
