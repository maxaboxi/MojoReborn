using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Identity.Domain.Entities;

namespace Mojo.Modules.Identity.Domain.Configurations;

public class UserSiteRoleAssignmentConfiguration : IEntityTypeConfiguration<UserSiteRoleAssignment>
{
    public void Configure(EntityTypeBuilder<UserSiteRoleAssignment> builder)
    {
        builder.ToTable("UserSiteRoleAssignments");

        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.HasOne(x => x.User)
            .WithMany(u => u.UserSiteRoleAssignments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Role)
            .WithMany(r => r.UserSiteRoleAssignments)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}