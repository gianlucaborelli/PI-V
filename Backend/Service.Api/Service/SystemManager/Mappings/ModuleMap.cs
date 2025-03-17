using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Service.Api.Service.SystemManager.Models;
namespace Service.Api.Service.SystemManager.Mappings;

public class ModuleMap : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.ToTable("Modules");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Tag)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.EspId)
            .HasMaxLength(50);

        builder.HasOne(m => m.Company)
            .WithMany(c => c.Modules)
            .HasForeignKey(m => m.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.Sensors)
            .WithOne(s => s.Module)
            .HasForeignKey(s => s.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
