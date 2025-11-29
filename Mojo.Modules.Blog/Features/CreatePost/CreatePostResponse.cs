using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.CreatePost;

public class CreatePostResponse : BaseResponse
{
    public Guid BlogPostId { get; set; }
}