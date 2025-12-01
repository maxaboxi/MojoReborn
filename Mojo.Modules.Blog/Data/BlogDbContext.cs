using Microsoft.EntityFrameworkCore;

namespace Mojo.Modules.Blog.Data;

public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
{
    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<BlogComment> BlogComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("mp_Blogs");

            entity.Property(e => e.Id).HasColumnName("ItemID").UseIdentityColumn();
            entity.Property(e => e.BlogPostId).HasColumnName("BlogGuid").ValueGeneratedOnAdd();
            entity.Property(e => e.Content).HasColumnName("Description");

            entity.Property(e => e.Author).HasColumnName("CreatedByUser").HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnName("CreatedDate").HasColumnType("datetime").HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Title).HasColumnName("Heading").HasMaxLength(255);
            entity.Property(e => e.Slug).HasColumnName("ItemUrl").HasMaxLength(255);
            entity.Property(e => e.ModifiedAt).HasColumnName("LastModUtc").HasColumnType("datetime");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.SubTitle).HasColumnName("SubTitle").HasMaxLength(500);
            entity.Property(e => e.LastModifiedBy).HasColumnName("LastModUserGuid");
            entity.Property(e => e.IsPublished).HasColumnName("Approved").HasDefaultValue(true);
        });

        modelBuilder.Entity<BlogPost>()
            .HasMany(b => b.Categories)
            .WithMany(c => c.BlogPosts)
            .UsingEntity<BlogItemCategory>(
                r => r.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId),
                l => l.HasOne<BlogPost>().WithMany().HasForeignKey(e => e.ItemId),
                j =>
                {
                    j.ToTable("mp_BlogItemCategories");
                    j.HasKey(e => e.Id);
                    j.Property(e => e.Id).HasColumnName("ID");
                    j.Property(e => e.ItemId).HasColumnName("ItemID");
                    j.Property(e => e.CategoryId).HasColumnName("CategoryID");
                });
        
        modelBuilder.Entity<BlogComment>(entity =>
        {
            entity.ToTable("mp_Comments");
            entity.HasKey(e => e.Guid);
            
            entity.HasQueryFilter(e => BlogPosts.Any(b => b.BlogPostId == e.ContentGuid));

            entity.HasIndex(e => e.ContentGuid, "IX_mp_Comments_1");
            entity.HasIndex(e => e.FeatureGuid, "IX_mp_Comments_2");
            entity.HasIndex(e => e.ModuleGuid, "IX_mp_Comments_3");
            entity.HasIndex(e => e.ParentGuid, "IX_mp_Comments_4");

            entity.Property(e => e.Guid).ValueGeneratedOnAdd();
            entity.Property(e => e.ContentGuid).HasColumnName("ContentGuid");
            entity.Property(e => e.Content).HasColumnName("UserComment");
            entity.Property(e => e.CreatedAt).HasColumnName("CreatedUtc").HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ModifiedAt).HasColumnName("LastModUtc").HasDefaultValueSql("(getutcdate())");
            
            entity.HasOne(c => c.BlogPost)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.ContentGuid)
                .HasPrincipalKey(b => b.BlogPostId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("mp_BlogCategories");

            entity.Property(e => e.Id).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasColumnName("Category").HasMaxLength(255);
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
        });
    }
    
}