using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Core.Features.SiteStructure.Entities;

namespace Mojo.Modules.Core.Features.SiteStructure.Configuration;

public class SiteHostConfiguration : IEntityTypeConfiguration<SiteHost>
{
    public void Configure(EntityTypeBuilder<SiteHost> builder)
    {
        builder.HasKey(e => e.HostId);

        builder.ToTable("mp_SiteHosts");

        builder.Property(e => e.HostId).HasColumnName("HostID");
        builder.Property(e => e.HostName).HasMaxLength(255);
        builder.Property(e => e.SiteId).HasColumnName("SiteID");
    }
}