using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Categories.DeleteCategory;

public class DeleteCategoryEndpoint
{
    [Authorize]
    [WolverineDelete("/api/{pageId}/blog/category/{categoryId}")]
    public static Task<DeleteCategoryResponse> Delete(
        int pageId,
        int categoryId,
        IMessageBus bus)
    {
        return bus.InvokeAsync<DeleteCategoryResponse>(new DeleteCategoryCommand(pageId, categoryId));
    }
}