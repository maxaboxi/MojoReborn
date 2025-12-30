using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.SiteStructure;

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
        var featureContextDto = await featureContextResolver.ResolveModule(query.PageId, FeatureNames.Forum, ct)
                                ?? throw new KeyNotFoundException();

        var thread = await db.ForumThreads
            .AsNoTracking()
            .Where(x =>
                x.Id == query.ThreadId &&
                x.ForumId == query.ForumId &&
                x.Forum.ModuleId == featureContextDto.ModuleId)
            .Select(x => new GetThreadResponse
            (
                x.Id,
                x.ForumId,
                x.ThreadSubject,
                x.ThreadGuid,
                x.StartedByUserId,
                x.Author.DisplayName,
                x.ForumPosts
                    .OrderBy(fp => fp.ThreadSequence)
                    .Where(fp => fp.ThreadSequence > query.LastThreadSequence)
                    .Take(query.Amount ?? 50)
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
            ))
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();

        return thread;
    }
}