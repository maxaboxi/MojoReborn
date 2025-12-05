using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Comments.DeleteComment;

public class DeleteBlogCommentHandler
{
    public static async Task<DeleteBlogCommentResponse> Handle(
        DeleteBlogCommentCommand command,
        BlogDbContext db,
        CancellationToken ct)
    {
        var comment = await db.BlogComments
            .Where(x => x.BlogPost.BlogPostId == command.BlogPostId && x.Id == command.BlogPostCommentId)
            .FirstOrDefaultAsync(ct);

        if (comment == null)
        {
            return BaseResponse.NotFound<DeleteBlogCommentResponse>("Comment not found.");
        }
        
        db.BlogComments.Remove(comment);
        await db.SaveChangesAsync(ct);
        
        return new DeleteBlogCommentResponse { IsSuccess = true, Message = "Comment deleted successfully." };
    }
}