using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Core.Features.Identity.Entities;

namespace Mojo.Modules.Core.Features.Identity.Configuration;

public class UserSiteProfileConfiguration : IEntityTypeConfiguration<UserSiteProfile>
{
    public void Configure(EntityTypeBuilder<UserSiteProfile> builder)
    {
        builder.ToTable("UserSiteProfiles");
        builder.HasKey(e => new { e.UserId, e.SiteId });
        
        builder.HasOne(e => e.User)
            .WithMany(e => e.UserSiteProfiles)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}