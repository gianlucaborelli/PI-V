using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Api.Service.SystemManager.Models;

namespace Service.Api.Service.SystemManager.Mappings
{
    public class RiskLimitMap : IEntityTypeConfiguration<RiskLimit>
    {
        public void Configure(EntityTypeBuilder<RiskLimit> builder)
        {
            //builder.ToTable("RiskLimits");

            //builder.HasOne(r => r.Location)
            //       .WithMany(location => location.RiskLimits)
            //       .HasForeignKey(r => r.LocationId)
            //       .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(rl => rl.Risk)
            //       .WithMany()
            //       .HasForeignKey(rl => rl.RiskId)
            //       .IsRequired(); 

        }
    }
}