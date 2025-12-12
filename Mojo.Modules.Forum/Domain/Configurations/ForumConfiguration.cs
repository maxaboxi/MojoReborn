using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Forum.Domain.Entities; 

namespace Mojo.Modules.Forum.Domain.Configurations;

public class ForumConfiguration : IEntityTypeConfiguration<ForumEntity>
{
    public void Configure(EntityTypeBuilder<ForumEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("mp_Forums");

        builder.Property(e => e.Id).HasColumnName("ItemID").UseIdentityColumn();
        builder.Property(e => e.AllowAnonymousPosts).HasDefaultValue(true, "DF_mp_Forums_AllowAnonymousPosts");
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedDate").HasColumnType("datetime").HasDefaultValueSql("(getutcdate())");
        builder.Property(e => e.ForumGuid).ValueGeneratedOnAdd();
        
        builder.Property(e => e.IsActive).HasDefaultValue(true, "DF_mp_ForumBoards_Active");
        builder.Property(e => e.ModuleId).HasColumnName("ModuleID");
        builder.Property(e => e.MostRecentPostDate).HasColumnType("datetime");
        builder.Property(e => e.MostRecentPostUserId)
            .HasDefaultValue(-1, "DF_mp_ForumBoards_MostRecentPostUserID")
            .HasColumnName("MostRecentPostUserID");
        builder.Property(e => e.PostsPerPage).HasDefaultValue(10, "DF_mp_Forums_EntriesPerPage");
        builder.Property(e => e.RolesThatCanPost).HasDefaultValue("Authenticated Users");
        builder.Property(e => e.SortOrder).HasDefaultValue(100, "DF_mp_ForumBoards_SortOrder");
        builder.Property(e => e.ThreadsPerPage).HasDefaultValue(40, "DF_mp_Forums_ThreadsPerPage");
        builder.Property(e => e.Title).HasMaxLength(100);
        builder.Property(e => e.Visible).HasDefaultValue(true);

        builder.HasMany(b => b.ForumThreads)
            .WithOne(b => b.Forum)
            .OnDelete(DeleteBehavior.Cascade);
    }
}