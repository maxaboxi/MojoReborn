using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Modules.Forum.Domain.Entities;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Forum.Features.Posts.CreatePost;

public class CreateForumPostHandler
{
    public static async Task<CreateForumPostResponse> Handle(
        CreateForumPostCommand command,
        ForumDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        if (securityContext.User.LegacyId == null)
        {
            throw new InvalidOperationException("LegacyId missing from the user.");
        }
        
        var thread = await db.ForumThreads
                .FromSqlRaw("SELECT * FROM mp_ForumThreads WITH (UPDLOCK) WHERE ThreadId = {0}", command.ThreadId)
                .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();

        var currentMaxSequence = (await db.ForumPosts.AsNoTracking()
            .Where(x => x.ThreadId == command.ThreadId)
            .MaxAsync(t => (int?)t.ThreadSequence, ct) ?? 0) + 1;

        ForumPost? originalPost = null;
        if (command.ReplyToPost.HasValue)
        {
            originalPost = await db.ForumPosts.FirstOrDefaultAsync(x => x.PostGuid == command.ReplyToPost.Value, ct);
        }

        var post = new ForumPost
        {
            ThreadId =  thread.Id,
            Subject = originalPost != null ? "Re: " + originalPost.Subject : "Re: " + thread.ThreadSubject,
            Post = command.Post,
            UserId = securityContext.User.LegacyId ?? 0,
            Approved = true,
            UserIp = command.UserIpAddress,
            ThreadSequence = currentMaxSequence,
        };

        thread.TotalReplies += 1;
        thread.MostRecentPostDate = DateTime.UtcNow;
        thread.MostRecentPostUserId = securityContext.User.LegacyId ?? 0;
        
        await db.ForumPosts.AddAsync(post, ct);
        await db.SaveChangesAsync(ct);

        if (!command.ReplyToPost.HasValue || originalPost == null)
            return new CreateForumPostResponse(post.Id);
        
        var reply = new ForumPostReplyLink
        {
            PostId = post.PostGuid,
            ParentPostId = originalPost.PostGuid
        };
            
        await db.ForumPostReplyLinks.AddAsync(reply, ct);
        await db.SaveChangesAsync(ct);

        return new CreateForumPostResponse(post.Id);
    }
}