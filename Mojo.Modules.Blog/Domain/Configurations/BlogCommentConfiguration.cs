using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Blog.Domain.Entities;

namespace Mojo.Modules.Blog.Domain.Configurations;

public class BlogCommentConfiguration : IEntityTypeConfiguration<BlogComment>
{
    public void Configure(EntityTypeBuilder<BlogComment> builder)
    {
        builder.ToTable("mp_Comments");
        builder.HasKey(e => e.Id);
        
        builder.HasIndex(e => e.ContentGuid, "IX_mp_Comments_1");
        builder.HasIndex(e => e.FeatureGuid, "IX_mp_Comments_2");
        builder.HasIndex(e => e.ModuleGuid, "IX_mp_Comments_3");
        builder.HasIndex(e => e.SiteGuid, "IX_mp_Comments");

        builder.Property(e => e.Id).HasColumnName("Guid").ValueGeneratedOnAdd();
        builder.Property(e => e.ContentGuid).HasColumnName("ContentGuid");
        builder.Property(e => e.Content).HasColumnName("UserComment");
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedUtc").HasDefaultValueSql("(getutcdate())");
        builder.Property(e => e.ModifiedAt).HasColumnName("LastModUtc").HasDefaultValueSql("(getutcdate())");
        builder.Property(e => e.UserIpAddress).HasColumnName("UserIp").HasMaxLength(50);
            
        builder.Property(e => e.Title).HasMaxLength(255);
        builder.Property(e => e.UserEmail).HasMaxLength(100);
        builder.Property(e => e.UserName).HasMaxLength(50);
            
        builder.HasOne(c => c.BlogPost)
            .WithMany(b => b.Comments)
            .HasForeignKey(c => c.ContentGuid)
            .HasPrincipalKey(b => b.BlogPostId);
        
        builder.Metadata.SetIsTableExcludedFromMigrations(true);
    }
}