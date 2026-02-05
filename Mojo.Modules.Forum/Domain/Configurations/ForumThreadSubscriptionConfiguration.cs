using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Forum.Domain.Entities;

namespace Mojo.Modules.Forum.Domain.Configurations;

public class ForumThreadSubscriptionConfiguration : IEntityTypeConfiguration<ForumThreadSubscription>
{
    public void Configure(EntityTypeBuilder<ForumThreadSubscription> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("mp_ForumThreadSubscriptions");

        builder.Property(e => e.Id).HasColumnName("ThreadSubscriptionID");
        builder.Property(e => e.SubscriptionGuid).ValueGeneratedOnAdd();
        builder.Property(e => e.SubscribeDate)
            .HasDefaultValueSql("(getdate())", "DF_mp_ForumThreadSubscriptions_SubscribeDate")
            .HasColumnType("datetime");
        builder.Property(e => e.ThreadId).HasColumnName("ThreadID");
        builder.Property(e => e.UnSubscribeDate).HasColumnType("datetime");
        builder.Property(e => e.UserId).HasColumnName("UserID");

        builder.HasOne(d => d.Thread).WithMany(p => p.ForumThreadSubscriptions)
            .HasForeignKey(d => d.ThreadId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_mp_ForumThreadSubscriptions_mp_ForumThreads");
    }
}