using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Comments.EditComment;

public class EditBlogCommentHandler
{
    public static async Task<EditBlogCommentResponse> Handle(
        EditBlogCommentCommand command,
        BlogDbContext db,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        CancellationToken ct)
    {
        var user = userService.GetUserAsync(claimsPrincipal, ct).Result;
        
        if (user == null)
        {
            return BaseResponse.Unauthorized<EditBlogCommentResponse>("User not found.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "BlogFeatureName", ct);
        
        if (featureContextDto == null)
        {
            return BaseResponse.Unauthorized<EditBlogCommentResponse>();
        }

        var comment = await db.BlogComments
            .Where(x => x.BlogPost.BlogPostId == command.BlogPostId && x.Id == command.BlogCommentId && x.UserGuid == user.Id)
            .FirstOrDefaultAsync(ct);

        if (comment == null)
        {
            return BaseResponse.NotFound<EditBlogCommentResponse>("Comment not found");
        }
        
        comment.Title = command.Title;
        comment.Content = command.Content;
        comment.ModifiedAt = DateTime.UtcNow;
        
        await db.SaveChangesAsync(ct);
        
        return new EditBlogCommentResponse { IsSuccess = true, BlogPostCommentId = comment.Id, Message = "Comment updated successfully." };
    }
}