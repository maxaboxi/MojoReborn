using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.SiteStructure.Domain.Entities;

namespace Mojo.Modules.SiteStructure.Domain.Configurations;

public class SiteHostConfiguration : IEntityTypeConfiguration<SiteHost>
{
    public void Configure(EntityTypeBuilder<SiteHost> builder)
    {
        builder.HasKey(e => e.HostId);

        builder.ToTable("mp_SiteHosts");

        builder.Property(e => e.HostId).HasColumnName("HostID");
        builder.Property(e => e.HostName).HasMaxLength(255);
        builder.Property(e => e.SiteId).HasColumnName("SiteID");

        builder.HasIndex(e => e.HostName);
    }
}