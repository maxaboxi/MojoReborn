using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Threads.GetThreads;

public class GetThreadsResponse : BaseResponse
{
    public List<ThreadDto> Threads { get; set; } = [];
}