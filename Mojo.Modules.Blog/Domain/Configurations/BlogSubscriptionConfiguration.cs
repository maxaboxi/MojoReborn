using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Blog.Domain.Entities;

namespace Mojo.Modules.Blog.Domain.Configurations;

public class BlogSubscriptionConfiguration : IEntityTypeConfiguration<BlogSubscription>
{
    public void Configure(EntityTypeBuilder<BlogSubscription> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.ToTable("BlogSubscriptions");
    }
}