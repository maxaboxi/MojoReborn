namespace Mojo.Modules.Blog.Features.Posts.GetPost;

public class BlogCommentDto
{
    public Guid Guid { get; set; }

    public Guid UserGuid { get; set; }

    public string? Title { get; set; }

    public string Content { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public Guid ModeratedBy { get; set; }

    public string? ModerationReason { get; set; }
}