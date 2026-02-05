using Mojo.Shared.Dtos.SiteStructure;

namespace Mojo.Shared.Interfaces.SiteStructure;

public interface IModuleResolver
{
    Task<List<ModuleDto>> GetAllModulesByFeatureName(string featureName, CancellationToken ct);
}