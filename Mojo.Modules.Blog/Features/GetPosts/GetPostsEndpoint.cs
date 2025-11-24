using Mojo.Shared.Features.Blog;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.GetPosts;

public class GetPostsEndpoint
{
    // The "WolverineGet" attribute does two things:
    // 1. It creates an HTTP GET route at /api/blog/posts
    // 2. It automatically maps the HTTP request to the GetPostsQuery
    
    [WolverineGet("/api/blog/posts")]
    public static Task<List<BlogPostDto>> Get(
        GetPostsQuery query, 
        IMessageBus bus)
    {
        // It simply delegates to the Handler
        return bus.InvokeAsync<List<BlogPostDto>>(query);
    }
}