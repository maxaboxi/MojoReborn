using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Posts.CreatePost;

public static class CreatePostEndpoint
{
    [Authorize]
    [WolverinePost("/api/blog/posts")]
    public static async Task<CreatePostResponse> Post(
        CreatePostCommand createPostCommand,
        IMessageBus bus)
    {
        var response = await bus.InvokeAsync<CreationResponse<CreatePostResponse>>(createPostCommand);
        return response.Value;
    }
}