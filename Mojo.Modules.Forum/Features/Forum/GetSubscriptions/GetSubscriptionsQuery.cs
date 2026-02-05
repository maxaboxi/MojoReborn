using Mojo.Shared.Domain;

namespace Mojo.Modules.Forum.Features.Forum.GetSubscriptions;

public record GetSubscriptionsQuery
{
    public string Name => FeatureNames.Forum;
}