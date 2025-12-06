namespace Mojo.Modules.Blog.Features.Posts.GetPosts;

public record BlogPostDto(Guid BlogPostGuid,  string Title, string Content, string Author, DateTime CreatedAt, List<string> Categories, int CommentCount);