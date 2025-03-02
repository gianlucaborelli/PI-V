using Microsoft.EntityFrameworkCore;

namespace Service.Api.Database
{
    public class ServiceDabatabaseContext : DbContext
    {
        public ServiceDabatabaseContext(DbContextOptions<ServiceDabatabaseContext> options) : base(options)
        {
        }
        //public DbSet<Models.Service> Services { get; set; }
    }
}
