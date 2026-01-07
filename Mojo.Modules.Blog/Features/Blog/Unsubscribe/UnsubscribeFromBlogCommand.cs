using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Blog.Unsubscribe;

public record UnsubscribeFromBlogCommand(int PageId, Guid SubscriptionId)
{
    public string Name => FeatureNames.Blog;
}