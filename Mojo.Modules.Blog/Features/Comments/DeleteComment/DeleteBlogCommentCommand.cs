namespace Mojo.Modules.Blog.Features.Comments.DeleteComment;

public record DeleteBlogCommentCommand(int PageId, Guid BlogPostId, Guid BlogPostCommentId);