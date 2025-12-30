namespace Mojo.Modules.Blog.Features.Blog.NotifySubscribers;

public record PostCreatedEvent(Guid ModuleGuid, Guid BlogPostId, Guid CreatedByUser, string Title, string Author, string Slug);