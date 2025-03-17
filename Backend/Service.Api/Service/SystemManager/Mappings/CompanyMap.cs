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
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Relacionamento 1:N com Module
            builder.HasMany(c => c.Modules)
                .WithOne(m => m.Company)
                .HasForeignKey(m => m.CompanyId)
                .OnDelete(DeleteBehavior.Cascade); // Remove módulos ao deletar a empresa

            // Mapeamento da lista de Tags como JSON no banco
            builder.Property(c => c.Tags)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
                )
                .HasColumnType("JSONB"); 
        }
    }
}
