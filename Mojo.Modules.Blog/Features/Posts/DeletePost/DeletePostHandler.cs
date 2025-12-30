using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

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
        var user = await userService.GetUserAsync(claimsPrincipal, ct) 
                   ?? throw new UnauthorizedAccessException();

        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, FeatureNames.Blog, ct)
                                ?? throw new KeyNotFoundException();
        
        if (!permissionService.CanEdit(user, featureContextDto))
        {
            throw new UnauthorizedAccessException();
        }
        
        var blogPost =  await db.BlogPosts
            .Where(x => 
                x.ModuleId == featureContextDto.ModuleId &&
                x.BlogPostId == command.Id && 
                x.Author == user.Email)
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();

        db.BlogPosts.Remove(blogPost);
        await db.SaveChangesAsync(ct);

        return new DeletePostResponse();
    }
}