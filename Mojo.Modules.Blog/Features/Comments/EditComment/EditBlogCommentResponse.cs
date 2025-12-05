using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Comments.EditComment;

public class EditBlogCommentResponse : BaseResponse
{
    public Guid BlogPostCommentId { get; set; }
}