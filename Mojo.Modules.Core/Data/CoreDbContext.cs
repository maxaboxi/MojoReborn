namespace Mojo.Modules.Core.Data;
using Microsoft.EntityFrameworkCore;

public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
    public DbSet<Page> Pages { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<PageModule> PageModules { get; set; }
    public DbSet<ModuleDefinition> ModuleDefinitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Page>(entity =>
        {
            entity.ToTable("mp_Pages");
            entity.HasKey(e => e.PageId);

            entity.HasIndex(e => e.PageName, "IX_mp_page_name");

            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.ParentId).HasColumnName("ParentID").HasDefaultValue(-1);
            entity.Property(e => e.PageName).HasColumnName("PageName");
            entity.Property(e => e.Url).HasColumnName("Url"); // Check SSMS, might be "Url" or "PageUrl"
            entity.Property(e => e.PageOrder).HasColumnName("PageOrder");
            entity.Property(e => e.AuthorizedRoles).HasColumnName("AuthorizedRoles");
            entity.Property(e => e.IncludeInMenu).HasColumnName("IncludeInMenu").HasDefaultValue(true);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.ToTable("mp_Modules");
            entity.HasKey(e => e.Id);
            
            entity.HasIndex(e => e.ModuleDefinitionId, "IX_mp_ModulesDefId");
            entity.HasIndex(e => e.ModuleGuid, "idxModulesGuid");

            entity.Property(e => e.Id).HasColumnName("ModuleID");
            entity.Property(e => e.ModuleDefinitionId).HasColumnName("ModuleDefID");
            entity.Property(e => e.ModuleGuid).HasColumnName("Guid");
            entity.Property(e => e.CreatedAt).HasColumnName("CreatedDate").HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Title).HasColumnName("ModuleTitle").HasMaxLength(255);
            entity.Property(e => e.AuthorizedEditRoles).HasColumnName("AuthorizedEditRoles");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID").HasDefaultValue(-1);
            
            entity.HasIndex(e => e.ModuleGuid, "idxModulesGuid");
        });

        modelBuilder.Entity<PageModule>(entity =>
        {
            entity.ToTable("mp_PageModules");
            entity.HasKey(e => new { e.PageId, e.ModuleId });
            
            entity.HasIndex(e => e.PaneName, "IX_mp_pm_pane");

            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.PaneName).HasMaxLength(50);
            entity.Property(e => e.ModuleOrder).HasDefaultValue(3);

            entity.HasOne(e => e.Page)
                .WithMany(p => p.PageModules)
                .HasForeignKey(pm => pm.PageId);
            
            entity.HasOne(e => e.Module)
                .WithMany(m => m.PageModules)
                .HasForeignKey(pm => pm.ModuleId);
        });
        
        modelBuilder.Entity<ModuleDefinition>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("mp_ModuleDefinitions");
            
            entity.HasIndex(e => e.ModuleDefinitionGuid, "idxModuleDefGuid");

            entity.Property(e => e.Id).HasColumnName("ModuleDefID");
            entity.Property(e => e.ControlSrc).HasMaxLength(255);
            entity.Property(e => e.FeatureName).HasMaxLength(255);
            entity.Property(e => e.ResourceFile).HasMaxLength(255);
            entity.Property(e => e.SearchListName).HasMaxLength(255);
            entity.Property(e => e.SortOrder).HasDefaultValue(500);
            
            entity.HasMany(d => d.Modules)
                .WithOne(m => m.ModuleDefinition)
                .HasForeignKey(m => m.ModuleDefinitionId);
        });
    }
}