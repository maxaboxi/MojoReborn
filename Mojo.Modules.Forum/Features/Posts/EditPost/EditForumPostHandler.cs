using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Forum.Features.Posts.EditPost;

public class EditForumPostHandler
{
    public static async Task<EditForumPostResponse> Handle(
        EditForumPostCommand command,
        ForumDbContext db,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        ILogger<EditForumPostHandler> logger,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct) ?? throw new UnauthorizedAccessException();
        
        if (user.LegacyId == null)
        {
            throw new InvalidOperationException("LegacyId missing from the user.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, FeatureNames.Forum, ct)
                                ?? throw new KeyNotFoundException();

        var existingPost = await db.ForumPosts.Include(x => x.Author).FirstOrDefaultAsync(
            x => 
                x.Thread.Forum.ModuleId == featureContextDto.ModuleId &&
                x.Thread.ForumId == command.ForumId &&
                x.ThreadId == command.ThreadId &&
                x.Id == command.PostId, ct) ?? throw new KeyNotFoundException();
        
        var hasAdminRights = permissionService.HasAdministratorRightsToThePage(user, featureContextDto);

        if (existingPost.Author.Id != user.LegacyId && !hasAdminRights)
        {
            throw new UnauthorizedAccessException();
        }
        
        existingPost.Post = !hasAdminRights ? command.Content : command.Content + "<p><em>[edited by Moderator]</em></p>";
        
        await db.SaveChangesAsync(ct);

        return new EditForumPostResponse(existingPost.Id);
    }
}