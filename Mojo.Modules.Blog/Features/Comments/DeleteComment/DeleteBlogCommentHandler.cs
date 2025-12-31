using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Comments.DeleteComment;

public class DeleteBlogCommentHandler
{
    public static async Task<DeleteBlogCommentResponse> Handle(
        DeleteBlogCommentCommand command,
        BlogDbContext db,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct) 
                   ?? throw new UnauthorizedAccessException();

        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, FeatureNames.Blog, ct)
                                ?? throw new KeyNotFoundException();
        
        var comment = await db.BlogComments
            .Where(x => 
                x.ModuleGuid == featureContextDto.ModuleGuid &&
                x.BlogPost.BlogPostId == command.BlogPostId && 
                x.Id == command.BlogPostCommentId)
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();
        
        var hasAdminRights = permissionService.HasAdministratorRightsToThePage(user, featureContextDto);

        if (comment.UserGuid != user.Id || !hasAdminRights)
        {
            throw new KeyNotFoundException();
        }

        if (hasAdminRights)
        {
            comment.Content = "[Deleted by Moderator]";
            comment.ModeratedBy = user.Id;
            comment.ModerationStatus = 1;
            comment.ModifiedAt = DateTime.UtcNow;
        }
        else
        {
            db.BlogComments.Remove(comment);
        }
        
        await db.SaveChangesAsync(ct);
        
        return new DeleteBlogCommentResponse();
    }
}