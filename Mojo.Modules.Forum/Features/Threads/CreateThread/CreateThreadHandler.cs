using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mojo.Modules.Forum.Data;
using Mojo.Modules.Forum.Domain.Entities;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Threads.CreateThread;

public class CreateThreadHandler
{
    public static async Task<CreateThreadResponse> Handle(
        CreateThreadCommand command,
        ForumDbContext db,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        ILogger<CreateThreadHandler> logger,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct);
        
        if (user?.LegacyId == null)
        {
            logger.LogError("User missing or user has no legacy id: {user}", user);
            return BaseResponse.Unauthorized<CreateThreadResponse>(user == null ? "User not found." : "Legacy account missing.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "ForumsFeatureName", ct);
        
        if (featureContextDto == null || !permissionService.CanEdit(user, featureContextDto))
        {
            return BaseResponse.Unauthorized<CreateThreadResponse>();
        }

        var currentMaxSequence = (await db.ForumThreads
            .Where(x => x.ForumId == command.ForumId)
            .MaxAsync(t => (int?)t.ForumSequence, ct) ?? 0) + 1;

        var thread = new ForumThread
        {
            ForumId =  command.ForumId,
            ThreadSubject = command.Subject,
            StartedByUserId = user.LegacyId ?? 0,
            ForumSequence = currentMaxSequence,
        };
        
        await db.ForumThreads.AddAsync(thread, ct);
        await db.SaveChangesAsync(ct);

        return new CreateThreadResponse { IsSuccess = true, ThreadId = thread.Id, Message = "Forum thread created successfully." };
    }
}