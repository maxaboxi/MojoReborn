using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Posts.DeletePost;

public class DeletePostHandler
{
    public static async Task<DeletePostResponse> Handle(
        DeletePostCommand command,
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
            return BaseResponse.Unauthorized<DeletePostResponse>("User not found.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "BlogFeatureName", ct);
        
        if (featureContextDto == null || !permissionService.CanEdit(user, featureContextDto))
        {
            return BaseResponse.Unauthorized<DeletePostResponse>();
        }
        
        var blogPost =  await db.BlogPosts
            .Where(x => 
                x.ModuleId == featureContextDto.ModuleId &&
                x.BlogPostId == command.Id && 
                x.Author == user.Email)
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(ct);

        if (blogPost == null)
        {
            return BaseResponse.NotFound<DeletePostResponse>("Blog post not found.");
        }

        db.BlogPosts.Remove(blogPost);
        await db.SaveChangesAsync(ct);
        
        return new DeletePostResponse{ IsSuccess = true, Message = "Blog post deleted successfully." };
    }
}