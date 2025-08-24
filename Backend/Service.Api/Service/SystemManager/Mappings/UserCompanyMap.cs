using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Api.Service.Authentication.Models;

namespace Service.Api.Service.SystemManager.Mappings;

public class UserCompanyMap : IEntityTypeConfiguration<UserCompany>
{
    public void Configure(EntityTypeBuilder<UserCompany> builder)
    {
        builder.ToTable("UserCompanies");

        builder.HasKey(uc => new { uc.UserId, uc.CompanyId });

        builder.HasOne(uc => uc.Company)
            .WithMany(c => c.UserCompanies)
            .HasForeignKey(uc => uc.CompanyId);
                
        builder.Property(uc => uc.UserId)
            .IsRequired();
    }
}
