using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Dtos.Subscriptions;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Forum.Features.Forum.GetSubscriptions;

public class GetSubscriptionsHandler
{
    public static async Task<GetSubscriptionsResponse> Handle(
        GetSubscriptionsQuery query,
        ForumDbContext db,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        IModuleResolver moduleResolver,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct)
                   ?? throw new UnauthorizedAccessException();

        var forumSubscriptions = await db.ForumSubscriptions.Where(x =>
                x.UserId == user.LegacyId)
            .ToListAsync(ct);

        if (forumSubscriptions.Count == 0)
        {
            return new GetSubscriptionsResponse { Subscriptions = [] };
        }
        
        var forumIds = forumSubscriptions.Select(x => x.ForumId).Distinct().ToList();
        
        var forums = await db.Forums
            .Where(x => forumIds.Contains(x.Id))
            .Select(x => new { x.Id, x.ModuleId })
            .ToListAsync(ct);
        
        var moduleIds = forums.Select(f => f.ModuleId).Distinct().ToList();
        
        var modules = await moduleResolver.GetAllModulesByFeatureName(FeatureNames.Forum, ct);
        
        var moduleLookup = modules
            .Where(m => moduleIds.Contains(m.ModuleId))
            .ToDictionary(m => m.ModuleId, m => m.ModuleGuid);
        
        var forumToModuleGuid = forums.ToDictionary(
            f => f.Id,
            f => moduleLookup.GetValueOrDefault(f.ModuleId, Guid.Empty));
        
        var subscriptions = forumSubscriptions
            .Where(x => forumToModuleGuid.TryGetValue(x.ForumId, out var guid) && guid != Guid.Empty)
            .Select(x =>
                new SubscriptionDto(
                    x.SubscriptionGuid,
                    forumToModuleGuid[x.ForumId],
                    FeatureNames.Forum,
                    x.SubscribeDate
                )).ToList();

        return new GetSubscriptionsResponse { Subscriptions = subscriptions };
    }
}