using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Threads.GetThreads;

public class GetThreadsEndpoint
{
    [WolverineGet("/api/{pageId}/forums/{forumId}/threads")]
    public static Task<GetThreadsResponse> Get(
        int pageId,
        int forumId,
        DateTime? lastThreadDate,
        int? lastThreadId,
        int amount,
        IMessageBus bus)
    {
        return bus.InvokeAsync<GetThreadsResponse>(new GetThreadsQuery(pageId, forumId, lastThreadDate, lastThreadId, amount));
    }
}