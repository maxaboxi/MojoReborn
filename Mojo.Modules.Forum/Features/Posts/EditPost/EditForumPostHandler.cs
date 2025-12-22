using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Posts.EditPost;

public class EditForumPostHandler
{
    public static async Task<EditForumPostResponse> Handle(
        EditForumPostCommand command,
        ForumDbContext db,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        ILogger<EditForumPostHandler> logger,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct);
        
        if (user?.LegacyId == null)
        {
            logger.LogError("User missing or user has no legacy id: {user}", user);
            return BaseResponse.Unauthorized<EditForumPostResponse>(user == null ? "User not found." : "Legacy account missing.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "ForumsFeatureName", ct);
        
        if (featureContextDto == null)
        {
            return BaseResponse.Unauthorized<EditForumPostResponse>();
        }

        var existingPost = await db.ForumPosts.Include(x => x.Author).FirstOrDefaultAsync(
            x => 
                x.Thread.Forum.ModuleId == featureContextDto.ModuleId &&
                x.Thread.ForumId == command.ForumId &&
                x.ThreadId == command.ThreadId &&
                x.Id == command.PostId, ct);

        if (existingPost == null)
        {
            return BaseResponse.NotFound<EditForumPostResponse>($"Post with id {command.PostId} not found.");
        }
        
        var hasAdminRights = permissionService.HasAdministratorRightsToThePage(user, featureContextDto);

        if (existingPost.Author.Id != user.LegacyId || !hasAdminRights)
        {
            return BaseResponse.Unauthorized<EditForumPostResponse>();
        }
        
        existingPost.Post = !hasAdminRights ? command.Content : command.Content + "<p><em>[edited by Moderator]</em></p>";
        
        await db.SaveChangesAsync(ct);

        return new EditForumPostResponse { IsSuccess = true, PostId = existingPost.Id, Message = "Thread updated successfully." };
    }
}