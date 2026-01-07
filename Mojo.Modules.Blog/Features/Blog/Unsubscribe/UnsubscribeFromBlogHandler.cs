using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Blog.Unsubscribe;

public class UnsubscribeFromBlogHandler
{
    public static async Task<UnsubscribeFromBlogResponse> Handle(
        UnsubscribeFromBlogCommand command,
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct) 
                   ?? throw new UnauthorizedAccessException();

        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, command.Name, ct)
                                ?? throw new KeyNotFoundException();
        
        var subscription = await db.BlogSubscriptions.FirstOrDefaultAsync(x => 
                               x.UserId == user.Id && 
                               x.ModuleGuid == featureContextDto.ModuleGuid &&
                               x.Id == command.SubscriptionId, ct) 
                           ?? throw new KeyNotFoundException("User is not subscribed to this blog.");
        
        db.BlogSubscriptions.Remove(subscription);
        await db.SaveChangesAsync(ct);

        return new UnsubscribeFromBlogResponse();
    }
}