namespace Mojo.Modules.Blog.Features.Posts.DeletePost;

public record DeletePostCommand(int PageId, Guid Id);