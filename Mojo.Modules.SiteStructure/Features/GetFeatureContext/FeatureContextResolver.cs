using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Mojo.Modules.SiteStructure.Data;
using Mojo.Shared.Dtos.SiteStructure;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.SiteStructure.Features.GetFeatureContext;

public class FeatureContextResolver(SiteStructureDbContext db, ISiteResolver siteResolver, IMemoryCache cache) : IFeatureContextResolver
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

    public async Task<FeatureContextDto?> ResolveModule(int id, string featureName, CancellationToken ct = default)
    {
        var site = await siteResolver.GetSite(ct);
        var cacheKey = $"featureContext:{site.SiteId}:{id}:{featureName}";

        return await cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = CacheDuration;
            return await db.PageModules
                .AsNoTracking()
                .Where(x => x.PageId == id &&
                            x.Module.SiteId == site.SiteId &&
                            x.Module.ModuleDefinition.FeatureName == featureName)
                .Select(x => 
                    new FeatureContextDto(
                            x.ModuleId, 
                            x.ModuleGuid, 
                            x.Module.FeatureGuid, 
                            x.Module.SiteId, 
                            x.Module.SiteGuid, 
                            new PageDto(x.Page.PageId, x.Page.AuthorizedRoles, x.Page.EditRoles)))
                .FirstOrDefaultAsync(ct);
        });
    }
}