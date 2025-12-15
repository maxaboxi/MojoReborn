using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Threads.GetThreads;

public class GetThreadsEndpoint
{
    [WolverineGet("/api/{pageId}/forums/threads")]
    public static Task<GetThreadsResponse> Get(
        int pageId,
        DateTime? lastThreadDate,
        int? lastThreadId,
        int amount,
        IMessageBus bus)
    {
        return bus.InvokeAsync<GetThreadsResponse>(new GetThreadsQuery(pageId, lastThreadDate, lastThreadId, amount));
    }
}