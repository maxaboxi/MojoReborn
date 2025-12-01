using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.DeletePost;

public class DeletePostEndpoint
{
    [WolverineDelete("/api/blog/posts/{blogPostId}")]
    public static Task<DeletePostResponse> Get(
        Guid blogPostId, 
        IMessageBus bus)
    {
        return bus.InvokeAsync<DeletePostResponse>(new DeletePostCommand(blogPostId));
    }
}