using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Threads.GetThread;

public class GetThreadEndpoint
{
    [WolverineGet("/api/{pageId}/forums/{forumId}/threads/{threadId}")]
    public static Task<GetThreadResponse> Get(
        int pageId,
        int forumId,
        int threadId,
        int amount,
        int lastThreadSequence,
        IMessageBus bus)
    {
        return bus.InvokeAsync<GetThreadResponse>(new GetThreadQuery(pageId, forumId, threadId, amount, lastThreadSequence));
    }
}