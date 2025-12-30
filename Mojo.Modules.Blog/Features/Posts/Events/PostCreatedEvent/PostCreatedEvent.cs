namespace Mojo.Modules.Blog.Features.Posts.Events.PostCreatedEvent;

public record PostCreatedEvent(Guid ModuleGuid, Guid BlogPostId, Guid CreatedByUser, string Title, string Author, string Slug);