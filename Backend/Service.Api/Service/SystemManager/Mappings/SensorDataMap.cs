using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Api.Service.SystemManager.Models;

namespace Service.Api.Service.SystemManager.Mappings
{
    public class SensorDataMap : IEntityTypeConfiguration<SensorData>
    {
        public void Configure(EntityTypeBuilder<SensorData> builder)
        {
            builder.HasKey(sd => sd.Id);

            builder.Property(sd => sd.Value)
                .IsRequired();

            builder.HasOne(sd => sd.Sensor)
                .WithMany(s => s.SensorDatas)
                .HasForeignKey(sd => sd.SensorId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
