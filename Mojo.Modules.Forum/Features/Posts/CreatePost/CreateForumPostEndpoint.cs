using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Posts.CreatePost;

public class CreateForumPostEndpoint
{
    [WolverinePost("/api/forums/threads/posts")]
    public static Task<CreateForumPostResponse> Post(
        CreateForumPostCommand command,
        HttpContext httpContext,
        IMessageBus bus)
    {
        command.UserIpAddress = httpContext.Connection.RemoteIpAddress?.ToString();
        return bus.InvokeAsync<CreateForumPostResponse>(command);
    }
}