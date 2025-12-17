using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Threads.EditThread;

public class EditThreadResponse : BaseResponse
{
    public int ThreadId { get; set; }
}