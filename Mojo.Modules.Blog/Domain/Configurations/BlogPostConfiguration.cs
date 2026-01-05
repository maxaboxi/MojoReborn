using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Blog.Domain.Entities;

namespace Mojo.Modules.Blog.Domain.Configurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("mp_Blogs");

        builder.Property(e => e.Id).HasColumnName("ItemID").UseIdentityColumn();
        builder.Property(e => e.BlogPostId).HasColumnName("BlogGuid").ValueGeneratedOnAdd();
        builder.Property(e => e.Content).HasColumnName("Description");

        builder.Property(e => e.Author).HasColumnName("CreatedByUser").HasMaxLength(100);
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedDate").HasColumnType("datetime").HasDefaultValueSql("(getutcdate())");
        builder.Property(e => e.Title).HasColumnName("Heading").HasMaxLength(255);
        builder.Property(e => e.Slug).HasColumnName("ItemUrl").HasMaxLength(255);
        builder.Property(e => e.ModifiedAt).HasColumnName("LastModUtc").HasColumnType("datetime");
        builder.Property(e => e.ModuleId).HasColumnName("ModuleID");
        builder.Property(e => e.SubTitle).HasColumnName("SubTitle").HasMaxLength(500);
        builder.Property(e => e.LastModifiedBy).HasColumnName("LastModUserGuid");
        builder.Property(e => e.IsPublished).HasColumnName("Approved").HasDefaultValue(true);

        builder.HasMany(b => b.Categories)
            .WithMany(c => c.BlogPosts)
            .UsingEntity<BlogPostCategory>();

        builder.HasIndex(e => new { e.ModuleGuid, e.Slug })
            .IsUnique();
    }
}