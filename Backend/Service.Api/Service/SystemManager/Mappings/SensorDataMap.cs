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

            builder.HasOne(sd => sd.Location)
                .WithMany(s => s.SensorDatas)
                .HasForeignKey(sd => sd.LocationId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
