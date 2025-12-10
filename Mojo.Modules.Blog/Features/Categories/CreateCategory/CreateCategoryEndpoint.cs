using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Categories.CreateCategory;

public class CreateCategoryEndpoint
{
    [Authorize]
    [WolverinePost("/api/blog/category")]
    public static Task<CreateCategoryResponse> Post(
        CreateCategoryCommand createCategoryCommand,
        IMessageBus bus)
    {
        return bus.InvokeAsync<CreateCategoryResponse>(createCategoryCommand);
    }
}