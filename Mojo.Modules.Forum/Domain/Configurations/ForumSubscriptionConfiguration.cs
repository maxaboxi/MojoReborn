using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Forum.Domain.Entities;

namespace Mojo.Modules.Forum.Domain.Configurations;

public class ForumSubscriptionConfiguration : IEntityTypeConfiguration<ForumSubscription>
{
    public void Configure(EntityTypeBuilder<ForumSubscription> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("mp_ForumSubscriptions");

        builder.Property(e => e.Id).HasColumnName("SubscriptionID");
        builder.Property(e => e.ForumId).HasColumnName("ForumID");
        builder.Property(e => e.SubscriptionGuid).ValueGeneratedOnAdd();
        builder.Property(e => e.SubscribeDate)
            .HasDefaultValueSql("(getdate())", "DF_mp_ForumSubscriptions_SubscribeDate")
            .HasColumnType("datetime");
        builder.Property(e => e.UnSubscribeDate).HasColumnType("datetime");
        builder.Property(e => e.UserId).HasColumnName("UserID");
    }
}