using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Identity.Domain.Entities;

namespace Mojo.Modules.Identity.Domain.Configurations;

public class LegacyRoleConfiguration : IEntityTypeConfiguration<LegacyRole>
{
    public void Configure(EntityTypeBuilder<LegacyRole> builder)
    {
        builder.HasKey(e => e.RoleId);

        builder.ToTable("mp_Roles");

        builder.Property(e => e.RoleId).HasColumnName("RoleID");
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.DisplayName).HasMaxLength(50);
        builder.Property(e => e.RoleName).HasMaxLength(50);
        builder.Property(e => e.SiteId).HasColumnName("SiteID");
        
        builder.Metadata.SetIsTableExcludedFromMigrations(true);
    }
}