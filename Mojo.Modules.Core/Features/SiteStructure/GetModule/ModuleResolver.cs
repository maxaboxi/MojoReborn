using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Core.Data;

namespace Mojo.Modules.Core.Features.SiteStructure.GetModule;

public class ModuleResolver(CoreDbContext db)
{
    public async Task<ModuleDto?> GetModuleByPageId(int id, CancellationToken ct = default)
    {
        return await db.PageModules
            .AsNoTracking()
            .Where(x => x.PageId == id)
            .Where(x => x.Module.ModuleDefinition.FeatureName == "BlogFeatureName")
            .Select(x => 
                new ModuleDto { Id = x.ModuleId, ModuleGuid = x.ModuleGuid, SiteGuid = x.Module.SiteGuid, SiteId = x.Module.SiteId, FeatureGuid = x.Module.FeatureGuid })
            .FirstOrDefaultAsync(ct);
    }
}