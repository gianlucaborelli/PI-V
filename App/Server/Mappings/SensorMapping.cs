using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Models;

namespace Server.Mappings
{
    public class SensorMapping : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("Sensors");

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.Id);

            builder.HasOne(s => s.Module)
                .WithMany(m => m.Sensors)
                .HasForeignKey(s => s.ModuleId);

            builder.HasMany(s => s.Datas)
                .WithOne(d => d.Sensor);
        }
    }
}
