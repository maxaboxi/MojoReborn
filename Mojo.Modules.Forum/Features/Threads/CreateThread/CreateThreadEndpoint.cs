using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Threads.CreateThread;

public class CreateThreadEndpoint
{
    [WolverinePost("/api/forums/threads/")]
    public static Task<CreateThreadResponse> Get(
        CreateThreadCommand command,
        IMessageBus bus)
    {
        return bus.InvokeAsync<CreateThreadResponse>(command);
    }
}