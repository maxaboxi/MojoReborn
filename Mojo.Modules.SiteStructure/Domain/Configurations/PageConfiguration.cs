using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.SiteStructure.Domain.Entities;

namespace Mojo.Modules.SiteStructure.Domain.Configurations;

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.ToTable("mp_Pages");
        builder.HasKey(e => e.PageId);

        builder.HasIndex(e => e.PageName, "IX_mp_page_name");

        builder.Property(e => e.PageId).HasColumnName("PageID");
        builder.Property(e => e.SiteId).HasColumnName("SiteID");
        builder.Property(e => e.ParentId).HasColumnName("ParentID").HasDefaultValue(-1);
        builder.Property(e => e.PageName).HasColumnName("PageName");
        builder.Property(e => e.Url).HasColumnName("Url");
        builder.Property(e => e.PageOrder).HasColumnName("PageOrder");
        builder.Property(e => e.AuthorizedRoles).HasColumnName("AuthorizedRoles");
        builder.Property(e => e.IncludeInMenu).HasColumnName("IncludeInMenu").HasDefaultValue(true);
    }
}