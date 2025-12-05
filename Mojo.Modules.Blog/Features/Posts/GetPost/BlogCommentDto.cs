namespace Mojo.Modules.Blog.Features.Posts.GetPost;

public record BlogCommentDto(Guid Id, Guid UserGuid, string UserName, string Title, string Content, DateTime CreatedAt, DateTime ModifiedAt, Guid ModeratedBy, string? ModerationReason);