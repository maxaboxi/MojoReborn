using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Notifications.Data;
using Mojo.Shared.Interfaces.Identity;

namespace Mojo.Modules.Notifications.Features.NotificationRead;

public class NotificationReadHandler
{
    public static async Task Handle(
        NotificationReadCommand command,
        NotificationsDbContext db,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct)
                   ?? throw new UnauthorizedAccessException();

        var notification = await db.UserNotifications.FirstOrDefaultAsync(
            n => n.Id == command.NotificationId && n.UserId == user.Id,
            ct) ?? throw new KeyNotFoundException("Notification not found");

        notification.IsRead = true;
        notification.UpdatedAt = DateTime.UtcNow;
        
        await db.SaveChangesAsync(ct);
    }
}