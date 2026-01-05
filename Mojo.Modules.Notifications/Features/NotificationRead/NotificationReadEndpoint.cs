using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Notifications.Features.NotificationRead;

public class NotificationReadEndpoint
{
    [Authorize]
    [WolverinePost("/api/notifications")]
    public static async Task Post(
        NotificationReadCommand command,
        IMessageBus bus)
    {
        await bus.InvokeAsync(command);
    }
}