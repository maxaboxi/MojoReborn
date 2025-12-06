using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Categories.GetCategories;

public class GetCategoriesEndpoint
{
    [WolverineGet("/api/blog/categories")]
    public static Task<GetCategoriesResponse> Get(
        int pageId,
        IMessageBus bus)
    {
        return bus.InvokeAsync<GetCategoriesResponse>(new GetCategoriesQuery(pageId));
    }
}