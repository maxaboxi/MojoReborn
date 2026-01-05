using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Contracts.Notifications;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Blog.NotifySubscribers;

public class PostCreatedHandler
{
    public static async Task<IEnumerable<SaveNotificationCommand>> Handle(
        PostCreatedEvent createdEvent,
        BlogDbContext db)
    {
        return await db.BlogSubscriptions
                .Where(x => x.ModuleGuid == createdEvent.ModuleGuid && x.UserId != createdEvent.CreatedByUser)
                .Select(x => 
                    new SaveNotificationCommand(
                        x.UserId, 
                        createdEvent.ModuleGuid,
                        $"New blogpost published by {createdEvent.Author}!", 
                        createdEvent.Slug, 
                        FeatureNames.Blog)
                    )
                .ToListAsync();
    }
}