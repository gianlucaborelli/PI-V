using Microsoft.EntityFrameworkCore;
using Service.Api.Service.Authentication.Models;
using Service.Api.Service.SystemManager.Mappings;
using Service.Api.Service.SystemManager.Models;
using Service.Api.Service.SystemManager.Models.Risks;

namespace Service.Api.Database
{
    public class ServiceDatabaseContext : DbContext
    {
        public ServiceDatabaseContext(DbContextOptions<ServiceDatabaseContext> options) : base(options)
        {
        }
        
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleAccessToken> ModuleAccessTokens { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<SensorData> SensorDatas { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        //public DbSet<RiskLimit> RiskLimits { get; set; }
        public DbSet<Risk> Risks { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("AppService");

            modelBuilder.Entity<Module>(new ModuleMap().Configure);
            modelBuilder.Entity<Location>(new LocationMap().Configure);
            modelBuilder.Entity<Company>(new CompanyMap().Configure);
            modelBuilder.Entity<SensorData>(new SensorDataMap().Configure);
            modelBuilder.Entity<UserCompany>(new UserCompanyMap().Configure);
            modelBuilder.Entity<RiskLimit>(new RiskLimitMap().Configure);
            modelBuilder.Entity<Risk>(new RiskMap().Configure);
        }
    }
}
