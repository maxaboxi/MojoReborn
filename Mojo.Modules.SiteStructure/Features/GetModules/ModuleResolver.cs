using Microsoft.EntityFrameworkCore;
using Mojo.Modules.SiteStructure.Data;
using Mojo.Shared.Dtos.SiteStructure;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.SiteStructure.Features.GetModules;

public class ModuleResolver(SiteStructureDbContext db) : IModuleResolver
{
    public async Task<List<ModuleDto>> GetAllModulesByFeatureName(string featureName, CancellationToken ct)
    {
        var moduleDefinitionIds = await db.ModuleDefinitions.Where(x => x.FeatureName == featureName)
            .Select(m => m.Id)
            .ToListAsync(ct);
        return await db.Modules
            .Where(x => moduleDefinitionIds.Contains(x.Id))
            .Select(x => new ModuleDto(x.Id, x.ModuleGuid)).ToListAsync(ct);
    }
}