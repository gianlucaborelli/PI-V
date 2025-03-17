using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Api.Service.SystemManager.Models;

namespace Service.Api.Service.SystemManager.Mappings
{
    public class SensorMap : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Description).HasMaxLength(500);

            builder.HasOne(s => s.Module)
                .WithMany(m => m.Sensors)
                .HasForeignKey(s => s.ModuleId);

            
            var converter = new ValueConverter<SensorType, string>(
                v => v.Name, 
                v => SensorType.FromName(v)
            );

            builder.Property(s => s.SensorType)
                .HasConversion(converter)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
