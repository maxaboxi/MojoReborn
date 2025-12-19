using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mojo.Modules.Forum.Data;
using Mojo.Modules.Forum.Domain.Entities;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

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
        ILogger<CreateForumPostHandler> logger,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct);
        
        if (user?.LegacyId == null)
        {
            logger.LogError("User missing or user has no legacy id: {user}", user);
            return BaseResponse.Unauthorized<CreateForumPostResponse>(user == null ? "User not found." : "Legacy account missing.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "ForumsFeatureName", ct);
        
        if (featureContextDto == null || !permissionService.CanEdit(user, featureContextDto))
        {
            return BaseResponse.Unauthorized<CreateForumPostResponse>();
        }
        
        var thread = await db.ForumThreads.FirstOrDefaultAsync(t => t.Id == command.ThreadId && t.ForumId == command.ForumId, ct);

        if (thread == null)
        {
            return BaseResponse.NotFound<CreateForumPostResponse>("Thread not found.");
        }

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
            return new CreateForumPostResponse
                { IsSuccess = true, PostId = post.Id, Message = "Forum post created successfully." };
        
        var reply = new ForumPostReplyLink
        {
            PostId = post.PostGuid,
            ParentPostId = originalPost.PostGuid
        };
            
        await db.ForumPostReplyLinks.AddAsync(reply, ct);
        await db.SaveChangesAsync(ct);

        return new CreateForumPostResponse { IsSuccess = true, PostId = post.Id, Message = "Forum post created successfully." };
    }
}