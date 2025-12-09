using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Comments.DeleteComment;

public class DeleteBlogCommentEndpoint
{
    [Authorize]
    [WolverineDelete("/api/{pageId}/blog/posts/{blogPostId}/comment/{blogCommentId}")]
    public static Task<DeleteBlogCommentResponse> Get(
        int pageId,
        Guid blogPostId,
        Guid blogCommentId, 
        IMessageBus bus)
    {
        return bus.InvokeAsync<DeleteBlogCommentResponse>(new DeleteBlogCommentCommand(pageId, blogPostId, blogCommentId));
    }
}