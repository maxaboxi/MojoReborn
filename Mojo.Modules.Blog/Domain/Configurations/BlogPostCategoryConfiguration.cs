using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Blog.Domain.Entities;

namespace Mojo.Modules.Blog.Domain.Configurations;

public class BlogPostCategoryConfiguration : IEntityTypeConfiguration<BlogPostCategory>
{
    public void Configure(EntityTypeBuilder<BlogPostCategory> builder)
    {
        builder.ToTable("mp_BlogItemCategories");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.ItemId).HasColumnName("ItemID");
        builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
        
        builder.HasOne<BlogPost>()
            .WithMany()
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<BlogCategory>()
            .WithMany()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}