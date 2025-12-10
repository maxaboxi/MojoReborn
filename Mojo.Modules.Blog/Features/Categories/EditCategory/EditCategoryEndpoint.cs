using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Categories.EditCategory;

public class EditCategoryEndpoint
{
    [Authorize]
    [WolverinePut("/api/blog/category")]
    public static Task<EditCategoryResponse> Put(
        EditCategoryCommand editCategoryCommand,
        IMessageBus bus)
    {
        return bus.InvokeAsync<EditCategoryResponse>(editCategoryCommand);
    }
}