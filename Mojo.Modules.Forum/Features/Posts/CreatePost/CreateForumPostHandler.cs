using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mojo.Modules.Forum.Data;
using Mojo.Modules.Forum.Domain.Entities;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Forum.Features.Posts.CreatePost;

public class CreateForumPostHandler
{
    public static async Task<CreateForumPostResponse> Handle(
        CreateForumPostCommand command,
        ForumDbContext db,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct) ?? throw new UnauthorizedAccessException();
        
        if (user.LegacyId == null)
        {
            throw new InvalidOperationException("LegacyId missing from the user.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, FeatureNames.Forum, ct)
                                ?? throw new KeyNotFoundException();
        
        if (!permissionService.CanEdit(user, featureContextDto))
        {
            throw new UnauthorizedAccessException();
        }
        
        var thread = await db.ForumThreads
            .FirstOrDefaultAsync(t => t.Id == command.ThreadId && t.ForumId == command.ForumId, ct) ??
                     throw new KeyNotFoundException();

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
            UserId = user.LegacyId ?? 0,
            Approved = true,
            UserIp = command.UserIpAddress,
            ThreadSequence = currentMaxSequence,
        };

        thread.TotalReplies += 1;
        thread.MostRecentPostDate = DateTime.UtcNow;
        thread.MostRecentPostUserId = user.LegacyId ?? 0;
        
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