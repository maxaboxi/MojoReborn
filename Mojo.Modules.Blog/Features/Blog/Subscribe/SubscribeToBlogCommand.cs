using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Blog.Features.Blog.Subscribe;

public record SubscribeToBlogCommand(int PageId) : IFeatureRequest
{
    public string Name => FeatureNames.Blog;
    public bool RequiresEditPermission => false;
    public bool UserCanEdit => false;
}