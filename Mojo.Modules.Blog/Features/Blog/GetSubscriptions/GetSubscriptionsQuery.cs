using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Blog.GetSubscriptions;

public record GetSubscriptionsQuery
{
    public string Name => FeatureNames.Blog;
}