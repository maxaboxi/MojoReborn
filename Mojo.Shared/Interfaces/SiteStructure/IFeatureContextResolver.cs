using Mojo.Shared.Dtos.SiteStructure;

namespace Mojo.Shared.Interfaces.SiteStructure;

public interface IFeatureContextResolver
{
    Task<FeatureContextDto?> ResolveModule(int pageId, string featureName, CancellationToken ct);
}