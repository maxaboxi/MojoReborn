using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Blog.Features.Posts.GetPost;

public static class GetPostEndpoint
{
    [WolverineGet("/api/blog/posts/{blogPostId}")]
    public static Task<GetPostResponse> Get(
        Guid blogPostId,
        int pageId,
        DateTime? lastCommentDate,
        int? amount,
        IMessageBus bus)
    {
        return bus.InvokeAsync<GetPostResponse>(new GetPostQuery(pageId, blogPostId, lastCommentDate, amount));
    }
}