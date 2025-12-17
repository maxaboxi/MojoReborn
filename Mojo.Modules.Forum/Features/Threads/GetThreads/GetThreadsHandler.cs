using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Threads.GetThreads;

public class GetThreadsHandler
{
    public static async Task<GetThreadsResponse> Handle(
        GetThreadsQuery query, 
        ForumDbContext db,
        IFeatureContextResolver featureContextResolver,
        CancellationToken ct)
    {
        var featureContextDto = await featureContextResolver.ResolveModule(query.PageId, "ForumsFeatureName", ct);
        
        if (featureContextDto == null)
        {
            return BaseResponse.NotFound<GetThreadsResponse>("Module not found.");
        }
        
        var threads = await db.ForumThreads.AsNoTracking()
            .Where(x => x.Forum.ModuleId == featureContextDto.ModuleId)
            .Where(x => query.LastThreadSequence == null || x.ForumSequence > query.LastThreadSequence)
            .OrderBy(x => x.ForumSequence)
            .Take(query.Amount ?? 20)
            .Select(x => new ThreadDto(
                x.Id,
                x.ForumId,
                x.ThreadSubject,
                x.CreatedAt,
                x.TotalViews,
                x.TotalReplies,
                x.SortOrder,
                x.IsLocked,
                x.MostRecentPostDate,
                x.MostRecentPostUserId,
                x.StartedByUserId,
                x.Author.DisplayName,
                x.ThreadGuid,
                x.LockedReason,
                x.LockedUtc
                ))
            .ToListAsync(ct);
        
        return new GetThreadsResponse { IsSuccess = true, Threads = threads };
    }
}