using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Blog.NotifySubscribers;

public record PostCreatedEvent(
    Guid ModuleGuid,
    Guid BlogPostGuid,
    int BlogPostId,
    Guid CreatedByUser,
    string Title,
    string Author,
    string Slug)
{
    public string Name => FeatureNames.Blog;
}