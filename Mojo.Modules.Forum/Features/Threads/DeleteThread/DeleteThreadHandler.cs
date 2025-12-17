using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Threads.DeleteThread;

public class DeleteThreadHandler
{
    public static async Task<DeleteThreadResponse> Handle(
        DeleteThreadCommand command,
        ForumDbContext db,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        ILogger<DeleteThreadHandler> logger,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct);
        
        if (user?.LegacyId == null)
        {
            logger.LogError("User missing or user has no legacy id: {user}", user);
            return BaseResponse.Unauthorized<DeleteThreadResponse>(user == null ? "User not found." : "Legacy account missing.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "ForumsFeatureName", ct);
        
        if (featureContextDto == null || !permissionService.CanEdit(user, featureContextDto))
        {
            return BaseResponse.Unauthorized<DeleteThreadResponse>();
        }

        var existingThread = await db.ForumThreads.FirstOrDefaultAsync(
            x => 
                x.Forum.ModuleId == featureContextDto.ModuleId &&
                x.Id == command.ThreadId && 
                x.ForumId == command.ForumId &&
                x.StartedByUserId == user.LegacyId, ct);

        if (existingThread == null)
        {
            return BaseResponse.NotFound<DeleteThreadResponse>($"Thread with id {command.ThreadId} not found.");
        }
        
        db.ForumThreads.Remove(existingThread);
        await db.SaveChangesAsync(ct);

        return new DeleteThreadResponse { IsSuccess = true, Message = "Thread deleted successfully." };
    }
}