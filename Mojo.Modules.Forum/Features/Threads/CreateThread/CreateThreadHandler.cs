using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Modules.Forum.Domain.Entities;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Forum.Features.Threads.CreateThread;

public class CreateThreadHandler
{
    public static async Task<CreateThreadResponse> Handle(
        CreateThreadCommand command,
        ForumDbContext db,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct) ?? throw new UnauthorizedAccessException();
        
        if (user.LegacyId == null)
        {
            throw new InvalidOperationException("LegacyId missing from the user.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, command.Name, ct)
                                ?? throw new KeyNotFoundException();
        
        if (!permissionService.CanEdit(user, featureContextDto))
        {
            throw new UnauthorizedAccessException();
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

        return new CreateThreadResponse(thread.Id);
    }
}