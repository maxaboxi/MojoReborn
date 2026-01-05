using Mojo.Shared.Dtos.Subscriptions;

namespace Mojo.Modules.Blog.Features.Blog.GetSubscriptions;

public class GetSubscriptionsResponse
{
    public List<SubscriptionDto> Subscriptions { get; set; } = [];
}