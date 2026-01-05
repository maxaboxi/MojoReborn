using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Notifications.Features.GetNotifications;

public class GetNotificationsEndpoint
{
    [Authorize]
    [WolverineGet("/api/notifications")]
    public static async Task<GetNotificationsResponse> Get(
        GetNotificationsQuery query,
        IMessageBus bus)
    {
        return await bus.InvokeAsync<GetNotificationsResponse>(query);
    }
}