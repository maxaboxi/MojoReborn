using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Forum.Features.Posts.EditPost;

public class EditForumPostHandler
{
    public static async Task<EditForumPostResponse> Handle(
        EditForumPostCommand command,
        ForumDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        if (securityContext.User.LegacyId == null)
        {
            throw new InvalidOperationException("LegacyId missing from the user.");
        }

        var existingPost = await db.ForumPosts.Include(x => x.Author).FirstOrDefaultAsync(
            x => 
                x.Thread.Forum.ModuleId == securityContext.FeatureContext.ModuleId &&
                x.Thread.ForumId == command.ForumId &&
                x.ThreadId == command.ThreadId &&
                x.Id == command.PostId, ct) ?? throw new KeyNotFoundException();
        
        if (existingPost.Author.Id != securityContext.User.LegacyId && !securityContext.IsAdmin)
        {
            throw new UnauthorizedAccessException();
        }
        
        existingPost.Post = !securityContext.IsAdmin ? command.Content : command.Content + "<p><em>[edited by Moderator]</em></p>";
        
        await db.SaveChangesAsync(ct);

        return new EditForumPostResponse(existingPost.Id);
    }
}