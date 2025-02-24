using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Models;

namespace Server.Mappings
{
    public class SensorDataMapping : IEntityTypeConfiguration<SensorData>
    {
        public void Configure(EntityTypeBuilder<SensorData> builder)
        {
            builder.ToTable("SensorDatas");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Id);

            builder.HasOne(d => d.Sensor)
                .WithMany(m => m.Datas);
        }
    }
}
