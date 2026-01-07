using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Blog.Domain.Entities;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Blog.Subscribe;

public class SubscribeToBlogHandler
{
    public static async Task<SubscribeToBlogResponse> Handle(
        SubscribeToBlogCommand command,
        BlogDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        if (await db.BlogSubscriptions.AnyAsync(x =>
                x.UserId == securityContext.User.Id && x.ModuleGuid == securityContext.FeatureContext.ModuleGuid, ct))
        {
            throw new BadHttpRequestException("User is already subscribed to this blog.");
        }
        
        var newSubscription = new BlogSubscription
        {
            SiteId = securityContext.FeatureContext.SiteId,
            SiteGuid = securityContext.FeatureContext.SiteGuid,
            ModuleId = securityContext.FeatureContext.ModuleId,
            ModuleGuid = securityContext.FeatureContext.ModuleGuid,
            UserId = securityContext.User.Id,
            CreatedAt = DateTime.UtcNow
        };

        await db.BlogSubscriptions.AddAsync(newSubscription, ct);
        await db.SaveChangesAsync(ct);

        return new SubscribeToBlogResponse();
    }
}