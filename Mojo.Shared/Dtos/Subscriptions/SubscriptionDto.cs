namespace Mojo.Shared.Dtos.Subscriptions;

public record SubscriptionDto(Guid Id, Guid ModuleGuid, string FeatureName, DateTime SubscribedAt);