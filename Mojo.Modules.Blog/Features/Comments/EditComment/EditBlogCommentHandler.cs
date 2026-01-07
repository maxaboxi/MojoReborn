using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Comments.EditComment;

public class EditBlogCommentHandler
{
    public static async Task<EditBlogCommentResponse> Handle(
        EditBlogCommentCommand command,
        BlogDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        var comment = await db.BlogComments
            .Where(x => 
                x.BlogPost.ModuleId == securityContext.FeatureContext.ModuleId &&
                x.BlogPost.BlogPostId == command.BlogPostId && 
                x.Id == command.BlogCommentId)
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();
        
        if (comment.UserGuid != securityContext.User.Id && !securityContext.IsAdmin)
        {
            throw new UnauthorizedAccessException();
        }
        
        comment.Title = command.Title;
        comment.Content = command.Content;
        comment.ModifiedAt = DateTime.UtcNow;

        if (comment.UserGuid != securityContext.User.Id && securityContext.IsAdmin)
        {
            comment.ModeratedBy = securityContext.User.Id;
            comment.ModerationStatus = 1;
            comment.ModerationReason = command.ModerationReason;
        }
        
        await db.SaveChangesAsync(ct);
        
        return new EditBlogCommentResponse(comment.Id);
    }
}