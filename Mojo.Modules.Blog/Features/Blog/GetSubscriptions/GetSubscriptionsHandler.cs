using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Dtos.Subscriptions;
using Mojo.Shared.Interfaces.Identity;

namespace Mojo.Modules.Blog.Features.Blog.GetSubscriptions;

public class GetSubscriptionsHandler
{
    public static async Task<GetSubscriptionsResponse> Handle(
        GetSubscriptionsQuery query,
        BlogDbContext db,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct)
                   ?? throw new UnauthorizedAccessException();

        var subscriptions = await db.BlogSubscriptions.Where(x =>
                x.UserId == user.Id)
            .Select(x => new SubscriptionDto(x.Id, x.ModuleGuid, FeatureNames.Blog, x.CreatedAt))
            .ToListAsync(ct);

        return new GetSubscriptionsResponse { Subscriptions = subscriptions };
    }
}