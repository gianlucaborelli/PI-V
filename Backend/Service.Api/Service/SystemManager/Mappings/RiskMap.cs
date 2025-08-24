using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Api.Service.SystemManager.Models.Risks;

namespace Service.Api.Service.SystemManager.Mappings;

public class RiskMap : IEntityTypeConfiguration<Risk>
{
    public void Configure(EntityTypeBuilder<Risk> builder)
    {
        builder.HasDiscriminator<string>("RiskType")
            .HasValue<TermoRisk>("TermoRisk");
    }
}
