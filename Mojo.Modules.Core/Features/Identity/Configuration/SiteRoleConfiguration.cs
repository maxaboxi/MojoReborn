using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Core.Features.Identity.Entities;

namespace Mojo.Modules.Core.Features.Identity.Configuration;

public class SiteRoleConfiguration : IEntityTypeConfiguration<SiteRole>
{
    public void Configure(EntityTypeBuilder<SiteRole> builder)
    {
        builder.ToTable("SiteRoles");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
        builder.Property(r => r.DisplayName).HasMaxLength(100);
        builder.HasIndex(r => r.SiteId);
    }
}