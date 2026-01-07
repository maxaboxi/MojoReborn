using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Comments.DeleteComment;

public class DeleteBlogCommentHandler
{
    public static async Task<DeleteBlogCommentResponse> Handle(
        DeleteBlogCommentCommand command,
        BlogDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        var comment = await db.BlogComments
            .Where(x => 
                x.ModuleGuid == securityContext.FeatureContext.ModuleGuid &&
                x.BlogPost.BlogPostId == command.BlogPostId && 
                x.Id == command.BlogPostCommentId)
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();
        
        if (comment.UserGuid != securityContext.User.Id && !securityContext.IsAdmin)
        {
            throw new UnauthorizedAccessException();
        }

        if (comment.UserGuid != securityContext.User.Id && securityContext.IsAdmin)
        {
            comment.Content = "[Deleted by Moderator]";
            comment.ModeratedBy = securityContext.User.Id;
            comment.ModerationStatus = 1;
        }
        else
        {
            comment.Content = "[Deleted by User]";
        }
        
        comment.ModifiedAt = DateTime.UtcNow;
        
        await db.SaveChangesAsync(ct);
        
        return new DeleteBlogCommentResponse();
    }
}