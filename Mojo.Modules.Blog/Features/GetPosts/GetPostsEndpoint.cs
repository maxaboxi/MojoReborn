using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.GetPosts;

public class GetPostsEndpoint
{
    [WolverineGet("/api/blog/posts")]
    public static Task<List<GetPostsResponse>> Get(
        GetPostsQuery query, 
        IMessageBus bus)
    {
        return bus.InvokeAsync<List<GetPostsResponse>>(query);
    }
}