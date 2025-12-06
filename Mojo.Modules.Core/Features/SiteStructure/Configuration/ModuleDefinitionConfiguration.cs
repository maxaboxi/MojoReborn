using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Core.Features.SiteStructure.Entities;

namespace Mojo.Modules.Core.Features.SiteStructure.Configuration;

public class ModuleDefinitionConfiguration : IEntityTypeConfiguration<ModuleDefinition>
{
    public void Configure(EntityTypeBuilder<ModuleDefinition> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("mp_ModuleDefinitions");
            
        builder.HasIndex(e => e.ModuleDefinitionGuid, "idxModuleDefGuid");

        builder.Property(e => e.Id).HasColumnName("ModuleDefID");
        builder.Property(e => e.ControlSrc).HasMaxLength(255);
        builder.Property(e => e.FeatureName).HasMaxLength(255);
        builder.Property(e => e.ResourceFile).HasMaxLength(255);
        builder.Property(e => e.SearchListName).HasMaxLength(255);
        builder.Property(e => e.SortOrder).HasDefaultValue(500);
            
        builder.HasMany(d => d.Modules)
            .WithOne(m => m.ModuleDefinition)
            .HasForeignKey(m => m.ModuleDefinitionId);
    }
}