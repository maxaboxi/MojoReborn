using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

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
        ILogger<EditThreadHandler> logger,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct);
        
        if (user?.LegacyId == null)
        {
            logger.LogError("User missing or user has no legacy id: {user}", user);
            return BaseResponse.Unauthorized<EditThreadResponse>(user == null ? "User not found." : "Legacy account missing.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "ForumsFeatureName", ct);
        
        if (featureContextDto == null || !permissionService.CanEdit(user, featureContextDto))
        {
            return BaseResponse.Unauthorized<EditThreadResponse>();
        }

        var existingThread = await db.ForumThreads.FirstOrDefaultAsync(
            x => 
                x.Forum.ModuleId == featureContextDto.ModuleId &&
                x.Id == command.ThreadId && 
                x.ForumId == command.ForumId, ct);

        if (existingThread == null)
        {
            return BaseResponse.NotFound<EditThreadResponse>($"Thread with id {command.ThreadId} not found.");
        }
        
        existingThread.ThreadSubject = command.Subject;
        
        await db.SaveChangesAsync(ct);

        return new EditThreadResponse { IsSuccess = true, ThreadId = existingThread.Id, Message = "Thread updated successfully." };
    }
}