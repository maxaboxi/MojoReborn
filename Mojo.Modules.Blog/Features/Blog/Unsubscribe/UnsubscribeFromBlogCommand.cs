using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Blog.Features.Blog.Unsubscribe;

public record UnsubscribeFromBlogCommand(int PageId, Guid SubscriptionId) : IFeatureRequest
{
    public string Name => FeatureNames.Blog;
    public bool RequiresEditPermission => false;
    public bool UserCanEdit => false;
}