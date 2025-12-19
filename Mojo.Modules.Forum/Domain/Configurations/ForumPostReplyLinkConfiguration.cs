using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Forum.Domain.Entities;

namespace Mojo.Modules.Forum.Domain.Configurations;

public class ForumPostReplyLinkConfiguration : IEntityTypeConfiguration<ForumPostReplyLink>
{
    public void Configure(EntityTypeBuilder<ForumPostReplyLink> builder)
    {
        builder.ToTable("ForumPostReplyLinks");

        builder.HasKey(x => x.PostId);
        
        builder.HasOne(x => x.Post)
            .WithOne()
            .HasForeignKey<ForumPostReplyLink>(x => x.PostId)
            .HasPrincipalKey<ForumPost>(x => x.PostGuid)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.ParentPost)
            .WithMany(x => x.Replies)
            .HasForeignKey(x => x.ParentPostId)
            .HasPrincipalKey(x => x.PostGuid)
            .OnDelete(DeleteBehavior.NoAction);
    }
}