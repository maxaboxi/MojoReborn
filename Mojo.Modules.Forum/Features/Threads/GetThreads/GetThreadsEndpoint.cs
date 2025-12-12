using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Threads.GetThreads;

public class GetThreadsEndpoint
{
    [WolverineGet("/api/forums/threads")]
    public static Task<GetThreadsResponse> Get(
        int pageId,
        int forumId,
        IMessageBus bus)
    {
        return bus.InvokeAsync<GetThreadsResponse>(new GetThreadsQuery(pageId, forumId));
    }
}