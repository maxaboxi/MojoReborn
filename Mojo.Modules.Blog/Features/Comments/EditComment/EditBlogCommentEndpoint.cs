using Microsoft.AspNetCore.Authorization;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Comments.EditComment;

public class EditBlogCommentEndpoint
{
    [Authorize]
    [WolverinePut("/api/blog/posts/comment")]
    public static Task<EditBlogCommentResponse> Put(
        EditBlogCommentCommand command,
        IMessageBus bus)
    {
        return bus.InvokeAsync<EditBlogCommentResponse>(command);
    }
}