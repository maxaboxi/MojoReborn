using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Threads.EditThread;

public class EditThreadEndpoint
{
    [WolverinePut("/api/forums/threads/")]
    public static Task<EditThreadResponse> Get(
        EditThreadCommand command,
        IMessageBus bus)
    {
        return bus.InvokeAsync<EditThreadResponse>(command);
    }
}