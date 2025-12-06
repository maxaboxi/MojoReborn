using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Blog.Domain.Entities;

namespace Mojo.Modules.Blog.Domain.Configurations;

public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("mp_BlogCategories");

        builder.Property(e => e.Id).HasColumnName("CategoryID");
        builder.Property(e => e.CategoryName).HasColumnName("Category").HasMaxLength(255);
        builder.Property(e => e.ModuleId).HasColumnName("ModuleID");
    }
}