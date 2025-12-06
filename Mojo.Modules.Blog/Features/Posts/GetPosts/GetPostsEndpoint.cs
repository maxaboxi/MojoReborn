using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Posts.GetPosts;

public class GetPostsEndpoint
{
    [WolverineGet("/api/blog/posts")]
    public static Task<GetPostsResponse> Get(
        int pageId,
        IMessageBus bus)
    {
        return bus.InvokeAsync<GetPostsResponse>(new GetPostsQuery(pageId));
    }
}