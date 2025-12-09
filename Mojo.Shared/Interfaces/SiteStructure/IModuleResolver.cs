using Mojo.Shared.Dtos.SiteStructure;

namespace Mojo.Shared.Interfaces.SiteStructure;

public interface IModuleResolver
{
    Task<ModuleDto?> ResolveModule(int pageId, string featureName, CancellationToken ct);
}