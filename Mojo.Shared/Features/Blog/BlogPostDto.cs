namespace Mojo.Shared.Features.Blog;

public class BlogPostDto
{
    public Guid BlogPostGuid { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<string> Categories { get; set; } = [];
    public int CommentCount { get; set; }
    public List<BlogCommentDto> Comments { get; set; } = [];
}