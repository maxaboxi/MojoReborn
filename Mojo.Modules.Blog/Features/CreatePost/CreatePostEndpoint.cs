using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.CreatePost;

public static class CreatePostEndpoint
{
    [WolverinePost("/api/blog/posts")]
    public static Task<CreatePostResponse> Post(
        CreatePostCommand createPostCommand,
        IMessageBus bus)
    {
        return bus.InvokeAsync<CreatePostResponse>(createPostCommand);
    }
}