using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.SiteStructure.Domain.Entities;

namespace Mojo.Modules.SiteStructure.Domain.Configurations;

public class PageModuleConfiguration : IEntityTypeConfiguration<PageModule>
{
    public void Configure(EntityTypeBuilder<PageModule> builder)
    {
        builder.ToTable("mp_PageModules");
        builder.HasKey(e => new { e.PageId, e.ModuleId });
            
        builder.HasIndex(e => e.PaneName, "IX_mp_pm_pane");

        builder.Property(e => e.PageId).HasColumnName("PageID");
        builder.Property(e => e.ModuleId).HasColumnName("ModuleID");
        builder.Property(e => e.PaneName).HasMaxLength(50);
        builder.Property(e => e.ModuleOrder).HasDefaultValue(3);

        builder.HasOne(e => e.Page)
            .WithMany(p => p.PageModules)
            .HasForeignKey(pm => pm.PageId);
            
        builder.HasOne(e => e.Module)
            .WithMany(m => m.PageModules)
            .HasForeignKey(pm => pm.ModuleId);
    }
}