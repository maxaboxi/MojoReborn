using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Comments.EditComment;

public class EditBlogCommentHandler
{
    public static async Task<EditBlogCommentResponse> Handle(
        EditBlogCommentCommand command,
        BlogDbContext db,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct) 
                   ?? throw new UnauthorizedAccessException();

        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, FeatureNames.Blog, ct)
                                ?? throw new KeyNotFoundException();

        var comment = await db.BlogComments
            .Where(x => 
                x.BlogPost.ModuleId == featureContextDto.ModuleId &&
                x.BlogPost.BlogPostId == command.BlogPostId && 
                x.Id == command.BlogCommentId)
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();
        
        var hasAdminRights = permissionService.HasAdministratorRightsToThePage(user, featureContextDto);

        if (comment.UserGuid != user.Id || !hasAdminRights)
        {
            throw new UnauthorizedAccessException();
        }
        
        comment.Title = command.Title;
        comment.Content = command.Content;
        comment.ModifiedAt = DateTime.UtcNow;

        if (comment.UserGuid != user.Id && hasAdminRights)
        {
            comment.ModeratedBy = user.Id;
            comment.ModerationStatus = 1;
            comment.ModerationReason = comment.ModerationReason;
        }
        
        await db.SaveChangesAsync(ct);
        
        return new EditBlogCommentResponse(comment.Id);
    }
}