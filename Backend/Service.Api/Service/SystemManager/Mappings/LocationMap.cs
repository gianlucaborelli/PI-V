using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Api.Service.SystemManager.Models;

namespace Service.Api.Service.SystemManager.Mappings
{
    public class LocationMap : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Description).HasMaxLength(500);

            builder.HasOne(s => s.Module)
                .WithMany(m => m.Locations)
                .HasForeignKey(s => s.ModuleId);

            // Configura a relação com RiskLimit
            builder.HasMany(s => s.RiskLimits)
                .WithMany(rl => rl.Locations);

            builder.HasMany(s => s.SensorDatas)
                .WithOne(sd => sd.Location)
                .HasForeignKey(sd => sd.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
