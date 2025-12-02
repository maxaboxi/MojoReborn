using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Posts.GetCategories;

public class GetCategoriesEndpoint
{
    [WolverineGet("/api/blog/posts/categories")]
    public static Task<List<GetCategoriesResponse>> Get(
        GetCategoriesQuery query, 
        IMessageBus bus)
    {
        return bus.InvokeAsync<List<GetCategoriesResponse>>(query);
    }
}