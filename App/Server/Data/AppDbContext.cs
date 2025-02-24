using Microsoft.EntityFrameworkCore;
using Server.Mappings;
using Shared.Models;

namespace Server.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Sensor> Sensors { get; set; } = null!;
    public DbSet<ModuleEsp> Modules { get; set; } = null!;
    public DbSet<SensorData> SensorDatas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Sensor>(new SensorMapping().Configure);
        modelBuilder.Entity<ModuleEsp>(new ModuleEspMapping().Configure);
        modelBuilder.Entity<SensorData>(new SensorDataMapping().Configure);
    }
}
