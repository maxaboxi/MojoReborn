using Mojo.Shared.Dtos.Subscriptions;

namespace Mojo.Modules.Forum.Features.Forum.GetSubscriptions;

public class GetSubscriptionsResponse
{
    public List<SubscriptionDto> Subscriptions { get; set; } = [];
}