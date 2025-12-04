using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Comments.CreateComment;

public class CreateBlogCommentResponse : BaseResponse
{
    public Guid CommentId { get; set; }
}