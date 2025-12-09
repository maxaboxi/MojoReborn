using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Core.Data;
using Mojo.Shared.Dtos.SiteStructure;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Core.Features.SiteStructure.GetModule;

public class ModuleResolver(CoreDbContext db) : IModuleResolver
{
    public async Task<ModuleDto?> ResolveModule(int id, string featureName, CancellationToken ct = default)
    {
        return await db.PageModules
            .AsNoTracking()
            .Where(x => x.PageId == id)
            .Where(x => x.Module.ModuleDefinition.FeatureName == featureName)
            .Select(x => 
                new ModuleDto
                {
                    Id = x.ModuleId, 
                    ModuleGuid = x.ModuleGuid, 
                    SiteGuid = x.Module.SiteGuid, 
                    SiteId = x.Module.SiteId, 
                    FeatureGuid = x.Module.FeatureGuid
                })
            .FirstOrDefaultAsync(ct);
    }
}