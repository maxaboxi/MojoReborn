using Microsoft.AspNetCore.Authorization;
using Mojo.Modules.Blog.Features.Posts.Events.PostCreatedEvent;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Posts.CreatePost;

public static class CreatePostEndpoint
{
    [Authorize]
    [WolverinePost("/api/blog/posts")]
    public static async Task<CreationResponse<CreatePostResponse>> Post(
        CreatePostCommand createPostCommand,
        IMessageBus bus)
    {
         var (response, _) = await bus.InvokeAsync<(CreationResponse<CreatePostResponse>, PostCreatedEvent)>(createPostCommand);
         return response;
    }
}