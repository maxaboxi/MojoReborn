using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Posts.CreatePost;

public static class CreatePostEndpoint
{
    [Authorize]
    [WolverinePost("/api/blog/posts")]
    public static Task<CreatePostResponse> Post(
        CreatePostCommand createPostCommand,
        IMessageBus bus)
    {
        return bus.InvokeAsync<CreatePostResponse>(createPostCommand);
    }
}