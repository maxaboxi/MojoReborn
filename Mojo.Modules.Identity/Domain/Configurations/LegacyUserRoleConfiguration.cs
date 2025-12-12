using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Identity.Domain.Entities;

namespace Mojo.Modules.Identity.Domain.Configurations;

public class LegacyUserRoleConfiguration : IEntityTypeConfiguration<LegacyUserRole>
{
    public void Configure(EntityTypeBuilder<LegacyUserRole> builder)
    {
        builder.ToTable("mp_UserRoles");

        builder.HasIndex(e => e.RoleId, "IX_UserRolesRoleID");

        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.RoleId).HasColumnName("RoleID");
        builder.Property(e => e.UserId).HasColumnName("UserID");
        
        builder.HasOne(e => e.User)
            .WithMany(e => e.UserRoles)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}