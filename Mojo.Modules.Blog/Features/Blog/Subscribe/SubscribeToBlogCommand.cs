using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Blog.Subscribe;

public record SubscribeToBlogCommand(int PageId)
{
    public string Name => FeatureNames.Blog;
}