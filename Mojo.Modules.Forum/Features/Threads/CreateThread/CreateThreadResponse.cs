using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Threads.CreateThread;

public class CreateThreadResponse : BaseResponse
{
    public int ThreadId { get; set; }
}