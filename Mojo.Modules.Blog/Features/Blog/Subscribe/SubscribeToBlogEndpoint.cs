using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Blog.Subscribe;

public class SubscribeToBlogEndpoint
{
    [Authorize]
    [WolverinePost("/api/blog/subscribe")]
    public static async Task<SubscribeToBlogResponse> Post(
        SubscribeToBlogCommand command,
        IMessageBus bus)
    {
        return await bus.InvokeAsync<SubscribeToBlogResponse>(command);
    }
}