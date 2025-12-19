using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Posts.EditPost;

public class EditForumPostResponse : BaseResponse
{
    public int PostId { get; set; }
}