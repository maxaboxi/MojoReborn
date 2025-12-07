using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Core.Features.SiteStructure.Entities;

namespace Mojo.Modules.Core.Features.SiteStructure.Configuration;

public class SiteConfiguration : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder.HasKey(e => e.SiteId).HasName("PK_Portals");

        builder.ToTable("mp_Sites");

        builder.HasIndex(e => e.SiteGuid, "idxSitesGuid");
        builder.Property(e => e.SiteId).HasColumnName("SiteID");
        builder.Property(e => e.SiteAlias).HasMaxLength(50);
        builder.Property(e => e.SiteName).HasMaxLength(255);
        builder.Property(e => e.Icon).HasMaxLength(50);
        builder.Property(e => e.Logo).HasMaxLength(50);
        
        builder.HasMany(e => e.Pages)
            .WithOne(e => e.Site)
            .HasForeignKey(f => f.SiteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.SiteHosts)
            .WithOne(e => e.Site)
            .HasForeignKey(f => f.SiteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}