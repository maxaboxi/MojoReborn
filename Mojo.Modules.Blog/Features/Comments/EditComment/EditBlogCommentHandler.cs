using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Comments.EditComment;

public class EditBlogCommentHandler
{
    public static async Task<EditBlogCommentResponse> Handle(
        EditBlogCommentCommand command,
        BlogDbContext db,
        CancellationToken ct)
    {
        var comment = await db.BlogComments
            .Where(x => x.BlogPost.BlogPostId == command.BlogPostId && x.Id == command.BlogCommentId)
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