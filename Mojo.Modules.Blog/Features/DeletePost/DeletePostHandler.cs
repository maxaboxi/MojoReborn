using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.DeletePost;

public class DeletePostHandler
{
    public static async Task<DeletePostResponse> Handle(
        DeletePostCommand command,
        BlogDbContext db,
        CancellationToken ct)
    {
        // TODO: authentication
        var blogPost =  await db.BlogPosts
            .Where(x => x.BlogPostId == command.Id)
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