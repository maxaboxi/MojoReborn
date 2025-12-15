using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Forum.Domain.Entities;

namespace Mojo.Modules.Forum.Domain.Configurations;

public class ForumThreadConfiguration : IEntityTypeConfiguration<ForumThread>
{
    public void Configure(EntityTypeBuilder<ForumThread> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("mp_ForumThreads");

        builder.Property(e => e.Id).HasColumnName("ThreadID");
        builder.Property(e => e.ForumId).HasColumnName("ForumID");
        builder.Property(e => e.ForumSequence).HasDefaultValue(1, "DF_mp_ForumThreads_ForumSequence");
        builder.Property(e => e.LockedReason).HasMaxLength(255);
        builder.Property(e => e.LockedUtc).HasColumnType("datetime");
        builder.Property(e => e.ModStatus).HasDefaultValue(1);
        builder.Property(e => e.MostRecentPostDate)
            .HasDefaultValueSql("(getutcdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.MostRecentPostUserId).HasColumnName("MostRecentPostUserID");

        builder.Property(e => e.SortOrder).HasDefaultValue(1000, "DF_mp_ForumThreads_SortOrder");
        builder.Property(e => e.StartedByUserId).HasColumnName("StartedByUserID");
        builder.Property(e => e.CreatedAt)
            .HasColumnName("ThreadDate")
            .HasDefaultValueSql("(getutcdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.ThreadGuid).ValueGeneratedOnAdd();
        builder.Property(e => e.ThreadSubject).HasMaxLength(255);

        builder.HasOne(d => d.Forum).WithMany(p => p.ForumThreads)
            .HasForeignKey(d => d.ForumId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_mp_ForumThreads_mp_Forums");

        builder.HasOne(x => x.Author).WithMany().HasForeignKey(t => t.StartedByUserId);
    }
}