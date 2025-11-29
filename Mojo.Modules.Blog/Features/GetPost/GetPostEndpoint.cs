using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.GetPost;

public static class GetPostEndpoint
{
    [WolverineGet("/api/blog/posts/{blogPostId}")]
    public static Task<GetPostResponse> Get(
        Guid blogPostId, 
        IMessageBus bus)
    {
        return bus.InvokeAsync<GetPostResponse>(new GetPostQuery(blogPostId));
    }
}