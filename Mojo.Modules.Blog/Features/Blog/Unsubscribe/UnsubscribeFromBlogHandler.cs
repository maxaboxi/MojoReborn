using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Blog.Unsubscribe;

public class UnsubscribeFromBlogHandler
{
    public static async Task<UnsubscribeFromBlogResponse> Handle(
        UnsubscribeFromBlogCommand command,
        BlogDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        var subscription = await db.BlogSubscriptions.FirstOrDefaultAsync(x => 
                               x.UserId == securityContext.User.Id && 
                               x.ModuleGuid == securityContext.FeatureContext.ModuleGuid &&
                               x.Id == command.SubscriptionId, ct) 
                           ?? throw new KeyNotFoundException("User is not subscribed to this blog.");
        
        db.BlogSubscriptions.Remove(subscription);
        await db.SaveChangesAsync(ct);

        return new UnsubscribeFromBlogResponse();
    }
}