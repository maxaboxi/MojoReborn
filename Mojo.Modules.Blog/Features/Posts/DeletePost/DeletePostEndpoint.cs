using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Posts.DeletePost;

public class DeletePostEndpoint
{
    [Authorize]
    [WolverineDelete("/api/{pageId}/blog/posts/{blogPostId}")]
    public static Task<DeletePostResponse> Delete(
        int pageId,
        Guid blogPostId, 
        IMessageBus bus)
    {
        return bus.InvokeAsync<DeletePostResponse>(new DeletePostCommand(pageId, blogPostId));
    }
}