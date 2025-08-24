using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Api.Service.SystemManager.Models;
using System.Text.Json;

namespace Service.Api.Service.SystemManager.Mappings
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.HasMany(c => c.Modules)
                .WithOne(m => m.Company)
                .HasForeignKey(m => m.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);            
        }
    }
}
