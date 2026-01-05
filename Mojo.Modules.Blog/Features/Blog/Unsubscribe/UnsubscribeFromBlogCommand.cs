namespace Mojo.Modules.Blog.Features.Blog.Unsubscribe;

public record UnsubscribeFromBlogCommand(int PageId, Guid SubscriptionId);