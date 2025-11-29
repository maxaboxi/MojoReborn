using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.EditPost;

public static class EditPostEndpoint
{
    [WolverinePut("/api/blog/posts")]
    public static Task<EditPostResponse> Put(
        EditPostCommand editPostCommand,
        IMessageBus bus)
    {
        return bus.InvokeAsync<EditPostResponse>(editPostCommand);
    }
}