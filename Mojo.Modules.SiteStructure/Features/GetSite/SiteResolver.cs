using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Mojo.Modules.SiteStructure.Data;
using Mojo.Shared.Dtos.SiteStructure;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.SiteStructure.Features.GetSite;

public class SiteResolver(SiteStructureDbContext db, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMemoryCache cache) : ISiteResolver
{
   private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

   public async Task<SiteDto> GetSite(CancellationToken ct)
   {
      var forcedSiteIdValue = configuration["ForceSiteId"];

      if (!string.IsNullOrEmpty(forcedSiteIdValue))
      {
         if (!int.TryParse(forcedSiteIdValue, out var forcedSiteId))
            throw new InvalidOperationException($"ForceSiteId config value '{forcedSiteIdValue}' is not a valid integer.");

         return await cache.GetOrCreateAsync($"site:forced:{forcedSiteId}", async entry =>
         {
            entry.SlidingExpiration = CacheDuration;
            return await db.Sites.AsNoTracking()
               .Where(x => x.SiteId == forcedSiteId)
               .Select(x => new SiteDto(x.SiteId, x.SiteGuid)).FirstOrDefaultAsync(ct)
               ?? throw new InvalidOperationException($"No sites configured in database with the forced site id: {forcedSiteIdValue}.");
         }) ?? throw new InvalidOperationException($"No sites configured in database with the forced site id: {forcedSiteIdValue}.");
      }
      
      var context = httpContextAccessor.HttpContext;

      if (context == null)
      {
         throw new InvalidOperationException("SiteResolver cannot be used outside of an HTTP request.");
      }
      
      var host = context.Request.Host.Host.ToLowerInvariant();

      // Check cache for this specific host
      if (cache.TryGetValue($"site:host:{host}", out SiteDto? cached) && cached != null)
         return cached;

      // Try host-specific DB lookups
      var site = await db.SiteHosts.AsNoTracking().Where(x => x.HostName == host)
         .Select(x => new SiteDto(x.SiteId, x.SiteGuid))
         .FirstOrDefaultAsync(ct);

      site ??= await db.Sites.AsNoTracking().Where(x => x.SiteName == host || x.SiteAlias == host)
         .Select(x => new SiteDto(x.SiteId, x.SiteGuid))
         .FirstOrDefaultAsync(ct);

      if (site != null)
      {
         // Only cache hosts that resolve to a known site — prevents unbounded growth from arbitrary Host headers
         cache.Set($"site:host:{host}", site, new MemoryCacheEntryOptions { SlidingExpiration = CacheDuration });
         return site;
      }

      // Unknown host — use shared cache key so all unrecognized hosts share one entry
      return await cache.GetOrCreateAsync("site:default", async entry =>
      {
         entry.SlidingExpiration = CacheDuration;
         return await db.Sites.AsNoTracking()
            .Where(x => x.SiteId == 1).Select(x => new SiteDto(x.SiteId, x.SiteGuid))
            .FirstOrDefaultAsync(ct)
            ?? throw new InvalidOperationException("No sites configured in database.");
      }) ?? throw new InvalidOperationException("No sites configured in database.");
   }
}