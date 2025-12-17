using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Threads.DeleteThread;

public class DeleteThreadEndpoint
{
    [WolverineDelete("/api/forums/threads/")]
    public static Task<DeleteThreadResponse> Delete(
        int pageId,
        int forumId,
        int threadId,
        IMessageBus bus)
    {
        return bus.InvokeAsync<DeleteThreadResponse>(new DeleteThreadCommand(pageId, forumId, threadId));
    }
}