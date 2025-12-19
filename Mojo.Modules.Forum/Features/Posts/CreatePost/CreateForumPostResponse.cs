using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Posts.CreatePost;

public class CreateForumPostResponse : BaseResponse
{
    public int PostId { get; set; }
}