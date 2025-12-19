using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Forum.Features.Posts.EditPost;

public class EditForumPostEndpoint
{
    [WolverinePut("/api/forums/threads/posts")]
    public static Task<EditForumPostResponse> Put(
        EditForumPostCommand command,
        IMessageBus bus)
    {
        return bus.InvokeAsync<EditForumPostResponse>(command);
    }
}