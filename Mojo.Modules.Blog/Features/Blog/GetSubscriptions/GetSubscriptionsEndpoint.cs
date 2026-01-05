using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Blog.GetSubscriptions;

public class GetSubscriptionsEndpoint
{
    [Authorize]
    [WolverineGet("/api/blog/subscriptions")]
    public static async Task<GetSubscriptionsResponse> Get(
        GetSubscriptionsQuery query,
        IMessageBus bus)
    {
        return await bus.InvokeAsync<GetSubscriptionsResponse>(query);
    }
}