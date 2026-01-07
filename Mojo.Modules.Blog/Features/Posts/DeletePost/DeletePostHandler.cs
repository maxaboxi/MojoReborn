using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
namespace Mojo.Modules.Blog.Features.Posts.DeletePost;

public class DeletePostHandler
{
    public static async Task<DeletePostResponse> Handle(
        DeletePostCommand command,
        BlogDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        var blogPost = await db.BlogPosts
            .Where(x => 
                x.ModuleId == securityContext.FeatureContext.ModuleId &&
                x.BlogPostId == command.Id)
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();

        if (blogPost.Author != securityContext.User.Email && !securityContext.IsAdmin)
        {
            throw new UnauthorizedAccessException();
        }

        db.BlogPosts.Remove(blogPost);
        await db.SaveChangesAsync(ct);

        return new DeletePostResponse();
    }
}