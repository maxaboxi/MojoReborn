using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Blog.Domain.Entities;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Blog.Subscribe;

public class SubscribeToBlogHandler
{
    public static async Task<SubscribeToBlogResponse> Handle(
        SubscribeToBlogCommand command,
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct) 
                   ?? throw new UnauthorizedAccessException();

        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, FeatureNames.Blog, ct)
                                ?? throw new KeyNotFoundException();

        if (await db.BlogSubscriptions.AnyAsync(x =>
                x.UserId == user.Id && x.ModuleGuid == featureContextDto.ModuleGuid, ct))
        {
            throw new BadHttpRequestException("User is already subscribed to this blog.");
        }
        
        var newSubscription = new BlogSubscription
        {
            SiteId = featureContextDto.SiteId,
            SiteGuid = featureContextDto.SiteGuid,
            ModuleId = featureContextDto.ModuleId,
            ModuleGuid = featureContextDto.ModuleGuid,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow
        };

        await db.BlogSubscriptions.AddAsync(newSubscription, ct);
        await db.SaveChangesAsync(ct);

        return new SubscribeToBlogResponse();
    }
}