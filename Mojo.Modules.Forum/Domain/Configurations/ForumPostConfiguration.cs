using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Forum.Domain.Entities;

namespace Mojo.Modules.Forum.Domain.Configurations;

public class ForumPostConfiguration : IEntityTypeConfiguration<ForumPost>
{
    public void Configure(EntityTypeBuilder<ForumPost> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("mp_ForumPosts");

        builder.HasIndex(e => e.UserId, "idxforumpostuid");

        builder.Property(e => e.Id).HasColumnName("PostID");
        builder.Property(e => e.ApprovedUtc).HasColumnType("datetime");
        builder.Property(e => e.ModStatus).HasDefaultValue(1);
        builder.Property(e => e.NotificationSent).HasDefaultValue(true);
        builder.Property(e => e.PostDate)
            .HasDefaultValueSql("(getutcdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.PostGuid).ValueGeneratedOnAdd();
        builder.Property(e => e.SortOrder).HasDefaultValue(100, "DF_mp_ForumPosts_SortOrder");
        builder.Property(e => e.Subject).HasMaxLength(255);
        builder.Property(e => e.ThreadId).HasColumnName("ThreadID");
        builder.Property(e => e.ThreadSequence).HasDefaultValue(1, "DF_mp_ForumPosts_ThreadSequence");
        builder.Property(e => e.UserId)
            .HasDefaultValue(-1, "DF_mp_ForumPosts_UserID")
            .HasColumnName("UserID");
        builder.Property(e => e.UserIp).HasMaxLength(50);

        builder.HasOne(d => d.Thread).WithMany(p => p.ForumPosts)
            .HasForeignKey(d => d.ThreadId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_mp_ForumPosts_mp_ForumThreads");

        builder.HasOne(x => x.Author).WithMany().HasForeignKey(x => x.UserId);
    }
}