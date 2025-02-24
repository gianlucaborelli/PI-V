using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Models;

namespace Server.Mappings
{
    public class ModuleEspMapping : IEntityTypeConfiguration<ModuleEsp>
    {
        public void Configure(EntityTypeBuilder<ModuleEsp> builder)
        {
            builder.ToTable("Modules");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Id);

            builder.HasMany(m => m.Sensors)
               .WithOne(s => s.Module)
               .HasForeignKey(s => s.ModuleId)  
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
