using Microsoft.EntityFrameworkCore;
using Mojo.Modules.SiteStructure.Data;
using Mojo.Shared.Dtos.SiteStructure;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.SiteStructure.Features.GetFeatureContext;

public class FeatureContextResolver(SiteStructureDbContext db) : IFeatureContextResolver
{
    public async Task<FeatureContextDto?> ResolveModule(int id, string featureName, CancellationToken ct = default)
    {
        return await db.PageModules
            .AsNoTracking()
            .Where(x => x.PageId == id)
            .Where(x => x.Module.ModuleDefinition.FeatureName == featureName)
            .Select(x => 
                new FeatureContextDto(
                        x.ModuleId, 
                        x.ModuleGuid, 
                        x.Module.FeatureGuid, 
                        x.Module.SiteId, 
                        x.Module.SiteGuid, 
                        new PageDto(x.Page.PageId, x.Page.AuthorizedRoles, x.Page.EditRoles)))
            .FirstOrDefaultAsync(ct);
    }
}