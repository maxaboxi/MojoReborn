using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Forum.GetSubscriptions;

public class GetSubscriptionsEndpoint
{
    [Authorize]
    [WolverineGet("/api/forum/subscriptions")]
    public static async Task<GetSubscriptionsResponse> Get(
        GetSubscriptionsQuery query,
        IMessageBus bus)
    {
        return await bus.InvokeAsync<GetSubscriptionsResponse>(query);
    }
}