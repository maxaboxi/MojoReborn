using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Comments.CreateComment;

public class CreateBlogCommentEndpoint
{
    [Authorize]
    [WolverinePost("/api/blog/posts/comment")]
    public static Task<CreateBlogCommentResponse> Post(
        CreateBlogCommentCommand command,
        HttpContext httpContext,
        IMessageBus bus)
    {
        command.UserIpAddress = httpContext.Connection.RemoteIpAddress?.ToString();
        return bus.InvokeAsync<CreateBlogCommentResponse>(command);
    }
}