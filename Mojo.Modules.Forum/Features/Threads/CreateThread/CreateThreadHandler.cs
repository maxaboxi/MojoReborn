using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Modules.Forum.Domain.Entities;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Forum.Features.Threads.CreateThread;

public class CreateThreadHandler
{
    public static async Task<CreateThreadResponse> Handle(
        CreateThreadCommand command,
        ForumDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        if (securityContext.User.LegacyId == null)
        {
            throw new InvalidOperationException("LegacyId missing from the user.");
        }

        var currentMaxSequence = (await db.ForumThreads
            .Where(x => x.ForumId == command.ForumId)
            .MaxAsync(t => (int?)t.ForumSequence, ct) ?? 0) + 1;

        var thread = new ForumThread
        {
            ForumId =  command.ForumId,
            ThreadSubject = command.Subject,
            StartedByUserId = securityContext.User.LegacyId ?? 0,
            ForumSequence = currentMaxSequence,
        };
        
        await db.ForumThreads.AddAsync(thread, ct);
        await db.SaveChangesAsync(ct);

        return new CreateThreadResponse(thread.Id);
    }
}