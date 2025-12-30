using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Contracts.Notifications;
using Mojo.Shared.Domain;
using Wolverine;

namespace Mojo.Modules.Blog.Features.Posts.Events.PostCreatedEvent;

public class PostCreatedHandler
{
    public static async Task Handle(
        PostCreatedEvent createdEvent,
        BlogDbContext db,
        IMessageBus bus)
    {
        var subscribedUsers = await db.BlogSubscriptions
                .Where(x => x.ModuleGuid == createdEvent.ModuleGuid && x.UserId != createdEvent.CreatedByUser)
                .Select(x => x.UserId)
                .ToListAsync();

        foreach (var id in subscribedUsers)
        {
            await bus.PublishAsync(
                new SendNotificationCommand(
                    id, 
                    $"New blogpost published by {createdEvent.Author}!", 
                    createdEvent.Slug, 
                    FeatureNames.Blog)
                );
        }
    }
}