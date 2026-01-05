using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Events;

namespace Mojo.Modules.Blog.Features.Blog.SubscriberDeleted;

public class SubscriberDeletedHandler
{
    public static async Task Handle(
        SubscriberDeletedEvent @event,
        BlogDbContext db,
        CancellationToken ct)
    {
        var subscriptions = await db.BlogSubscriptions
            .Where(x => x.UserId == @event.UserId)
            .ToListAsync(ct);
        
        db.BlogSubscriptions.RemoveRange(subscriptions);
        await db.SaveChangesAsync(ct);
    }
}