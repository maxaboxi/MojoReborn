namespace Mojo.Modules.Core.Data;
using Microsoft.EntityFrameworkCore;

public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
    public DbSet<Page> Pages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Page>(entity =>
        {
            entity.ToTable("mp_Pages");
            entity.HasKey(e => e.PageId);

            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.PageName).HasColumnName("PageName");
            entity.Property(e => e.Url).HasColumnName("Url"); // Check SSMS, might be "Url" or "PageUrl"
            entity.Property(e => e.PageOrder).HasColumnName("PageOrder");
            entity.Property(e => e.AuthorizedRoles).HasColumnName("AuthorizedRoles");
            entity.Property(e => e.IncludeInMenu).HasColumnName("IncludeInMenu");
        });
    }
}