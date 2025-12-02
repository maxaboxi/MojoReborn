using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Posts.CreatePost;

public class CreatePostResponse : BaseResponse
{
    public Guid BlogPostId { get; set; }
}