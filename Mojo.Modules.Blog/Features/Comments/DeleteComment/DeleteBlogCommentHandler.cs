using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Comments.DeleteComment;

public class DeleteBlogCommentHandler
{
    public static async Task<DeleteBlogCommentResponse> Handle(
        DeleteBlogCommentCommand command,
        BlogDbContext db,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct);
        
        if (user == null)
        {
            return BaseResponse.Unauthorized<DeleteBlogCommentResponse>("User not found.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "BlogFeatureName", ct);
        
        if (featureContextDto == null)
        {
            return BaseResponse.Unauthorized<DeleteBlogCommentResponse>();
        }
        
        var comment = await db.BlogComments
            .Where(x => 
                x.ModuleGuid == featureContextDto.ModuleGuid &&
                x.BlogPost.BlogPostId == command.BlogPostId && 
                x.Id == command.BlogPostCommentId)
            .FirstOrDefaultAsync(ct);

        if (comment == null)
        {
            return BaseResponse.NotFound<DeleteBlogCommentResponse>("Comment not found.");
        }
        
        var hasAdminRights = permissionService.HasAdministratorRightsToThePage(user, featureContextDto);

        if (comment.UserGuid != user.Id || !hasAdminRights)
        {
            return BaseResponse.Unauthorized<DeleteBlogCommentResponse>();
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
        
        return new DeleteBlogCommentResponse { IsSuccess = true, Message = "Comment deleted successfully." };
    }
}