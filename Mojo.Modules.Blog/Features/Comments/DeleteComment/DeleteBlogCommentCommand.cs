namespace Mojo.Modules.Blog.Features.Comments.DeleteComment;

public record DeleteBlogCommentCommand(Guid BlogPostId, Guid BlogPostCommentId);