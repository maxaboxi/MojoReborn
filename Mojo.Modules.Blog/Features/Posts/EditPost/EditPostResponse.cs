using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Posts.EditPost;

public class EditPostResponse : BaseResponse
{
    public Guid BlogPostId { get; set; }
}