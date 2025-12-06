using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Core.Features.Identity.Entities;

namespace Mojo.Modules.Core.Features.Identity.Configuration;

public class LegacyUserConfiguration : IEntityTypeConfiguration<LegacyUser>
{
    public void Configure(EntityTypeBuilder<LegacyUser> builder)
    {
        builder.ToTable("mp_Users");
        builder.HasKey(e => e.UserId);
        
        builder.Property(e => e.UserId).HasColumnName("UserID");
        builder.Property(e => e.SiteId).HasColumnName("SiteID");
        builder.Property(e => e.CreatedAt).HasColumnName("DateCreated");
    }
}