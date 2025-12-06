using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Core.Features.SiteStructure.Entities;

namespace Mojo.Modules.Core.Features.SiteStructure.Configuration;

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.ToTable("mp_Modules");
        builder.HasKey(e => e.Id);
            
        builder.HasIndex(e => e.ModuleDefinitionId, "IX_mp_ModulesDefId");
        builder.HasIndex(e => e.ModuleDefinitionId, "idxModulesMDef");
        builder.HasIndex(e => e.ModuleGuid, "idxModulesGuid");
        builder.HasIndex(e => e.SiteId, "IX_mp_ModulesSiteID");
        builder.HasIndex(e => e.FeatureGuid, "IX_mp_ModulesFeatGuid");
        builder.HasIndex(e => e.SiteGuid, "IX_mp_ModulesSiteGuid");
        builder.HasIndex(e => e.FeatureGuid, "idxModulesFGuid");

        builder.HasIndex(e => e.SiteId, "idxModulesSID");

        builder.Property(e => e.Id).HasColumnName("ModuleID");
        builder.Property(e => e.SiteId).HasColumnName("SiteID");
        builder.Property(e => e.ModuleDefinitionId).HasColumnName("ModuleDefID");
        builder.Property(e => e.ModuleGuid).HasColumnName("Guid");
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedDate").HasDefaultValueSql("(getdate())");
        builder.Property(e => e.Title).HasColumnName("ModuleTitle").HasMaxLength(255);
        builder.Property(e => e.AuthorizedEditRoles).HasColumnName("AuthorizedEditRoles");
        builder.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID").HasDefaultValue(-1);
            
        builder.HasIndex(e => e.ModuleGuid, "idxModulesGuid");
    }
}