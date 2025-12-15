using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Threads.GetThread;

public class GetThreadHandler
{
    public static async Task<GetThreadResponse> Handle(
        GetThreadQuery query,
        ForumDbContext db,
        IFeatureContextResolver featureContextResolver,
        CancellationToken ct
        )
    {
        var featureContextDto = await featureContextResolver.ResolveModule(query.PageId, "ForumsFeatureName", ct);
        
        if (featureContextDto == null)
        {
            return BaseResponse.NotFound<GetThreadResponse>("Module not found.");
        }

        var thread = await db.ForumThreads
            .AsNoTracking()
            .Where(x =>
                x.Id == query.ThreadId &&
                x.ForumId == query.ForumId &&
                x.Forum.ModuleId == featureContextDto.ModuleId)
            .Select(x => new GetThreadResponse
            {
                Id = x.Id,
                ForumId = x.ForumId,
                Subject = x.ThreadSubject,
                ThreadGuid = x.ThreadGuid,
                UserId = x.StartedByUserId,
                UserName = x.Author.DisplayName,
                ForumPosts = x.ForumPosts
                    .OrderBy(fp => fp.ThreadSequence)
                    .Where(fp => fp.ThreadSequence > query.LastThreadSequence)
                    .Take(query.Amount)
                    .Select(fp => new ForumPostDto(
                        x.ForumId,
                        fp.Id,
                        fp.PostGuid,
                        fp.ThreadId,
                        fp.ThreadSequence,
                        x.ThreadSubject,
                        fp.Post ?? "[deleted]",
                        fp.AnswerVotes,
                        fp.UserId,
                        fp.Author.DisplayName,
                        fp.Replies.FirstOrDefault(reply => fp.PostGuid == reply.ParentPostId)!.ParentPostId,
                        fp.PostDate)).ToList()
            })
            .FirstOrDefaultAsync(ct);

        return thread ?? BaseResponse.NotFound<GetThreadResponse>("Thread not found.");
    }
}