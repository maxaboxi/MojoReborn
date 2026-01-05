using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Notifications.Data;
using Mojo.Modules.Notifications.Features.GetNotifications;

namespace Mojo.Modules.Notifications.Features.DeleteNotifications;

public class DeleteNotificationsHandler
{
    public static async Task Handle(
        DeleteNotificationsEvent @event,
        NotificationsDbContext db,
        CancellationToken ct)
    {
        await db.UserNotifications
            .Where(x => x.IsRead && x.CreatedAt < DateTime.UtcNow.AddDays(-@event.RetentionDays))
            .ExecuteDeleteAsync(ct);
    }
}