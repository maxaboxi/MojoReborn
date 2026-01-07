using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Forum.Features.Threads.EditThread;

public class EditThreadHandler
{
    public static async Task<EditThreadResponse> Handle(
        EditThreadCommand command,
        ForumDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        if (securityContext.User.LegacyId == null)
        {
            throw new InvalidOperationException("LegacyId missing from the user.");
        }

        var existingThread = await db.ForumThreads.FirstOrDefaultAsync(
            x => 
                x.Forum.ModuleId == securityContext.FeatureContext.ModuleId &&
                x.Id == command.ThreadId && 
                x.ForumId == command.ForumId &&
                x.StartedByUserId == securityContext.User.LegacyId, ct) ?? throw new KeyNotFoundException();
        
        existingThread.ThreadSubject = command.Subject;
        
        await db.SaveChangesAsync(ct);

        return new EditThreadResponse(existingThread.Id);
    }
}