using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Forum.Features.Threads.EditThread;

public class EditThreadHandler
{
    public static async Task<EditThreadResponse> Handle(
        EditThreadCommand command,
        ForumDbContext db,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct) ?? throw new UnauthorizedAccessException();
        
        if (user.LegacyId == null)
        {
            throw new InvalidOperationException("LegacyId missing from the user.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, FeatureNames.Forum, ct)
                                ?? throw new KeyNotFoundException();
        
        if (!permissionService.CanEdit(user, featureContextDto))
        {
            throw new UnauthorizedAccessException();
        }

        var existingThread = await db.ForumThreads.FirstOrDefaultAsync(
            x => 
                x.Forum.ModuleId == featureContextDto.ModuleId &&
                x.Id == command.ThreadId && 
                x.ForumId == command.ForumId &&
                x.StartedByUserId == user.LegacyId, ct) ?? throw new KeyNotFoundException();
        
        existingThread.ThreadSubject = command.Subject;
        
        await db.SaveChangesAsync(ct);

        return new EditThreadResponse(existingThread.Id);
    }
}