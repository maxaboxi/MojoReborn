using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Blog.Unsubscribe;

public class UnsubscribeFromBlogEndpoint
{
    [Authorize]
    [WolverinePost("/api/blog/unsubscribe")]
    public static async Task<UnsubscribeFromBlogResponse> Post(
        UnsubscribeFromBlogCommand command,
        IMessageBus bus)
    {
        return await bus.InvokeAsync<UnsubscribeFromBlogResponse>(command);
    }
}