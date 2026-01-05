using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Notifications.Data;
using Mojo.Shared.Interfaces.Identity;

namespace Mojo.Modules.Notifications.Features.GetNotifications;

public class GetNotificationsHandler
{
    public static async Task<GetNotificationsResponse> Handle(
        GetNotificationsQuery query,
        NotificationsDbContext db,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct)
                   ?? throw new UnauthorizedAccessException();

        var notifications = await db.UserNotifications
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new NotificationDto(
                x.Id, x.Message, x.Url, x.IsRead, x.CreatedAt))
            .ToListAsync(ct);

        return new GetNotificationsResponse { Notifications = notifications };
    }
}