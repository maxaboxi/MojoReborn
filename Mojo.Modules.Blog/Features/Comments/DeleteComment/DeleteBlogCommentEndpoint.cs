using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Comments.DeleteComment;

public class DeleteBlogCommentEndpoint
{
    [WolverineDelete("/api/blog/posts/{blogPostId}/comment/{blogCommentId}")]
    public static Task<DeleteBlogCommentResponse> Get(
        Guid blogPostId,
        Guid blogCommentId, 
        IMessageBus bus)
    {
        return bus.InvokeAsync<DeleteBlogCommentResponse>(new DeleteBlogCommentCommand(blogPostId, blogCommentId));
    }
}