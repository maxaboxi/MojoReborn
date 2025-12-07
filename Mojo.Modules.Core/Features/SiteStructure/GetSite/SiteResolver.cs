using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Core.Data;

namespace Mojo.Modules.Core.Features.SiteStructure.GetSite;

public class SiteResolver(CoreDbContext db, IHttpContextAccessor httpContextAccessor)
{
   private SiteDto? _resolvedSite;
   
   public async Task<SiteDto> GetSite(CancellationToken ct)
   {
      if (_resolvedSite != null)
      {
         return _resolvedSite;
      }
      
      var context = httpContextAccessor.HttpContext;

      if (context == null)
      {
         throw new InvalidOperationException("SiteResolver cannot be used outside of an HTTP request.");
      }
      
      var host = context.Request.Host.Host;
      
      _resolvedSite = (await db.SiteHosts.AsNoTracking().Where(x => x.HostName == host)
         .Select(x => new SiteDto
         {
            SiteId = x.SiteId,
            SiteGuid =  x.SiteGuid
         })
         .FirstOrDefaultAsync(ct) ?? await db.Sites.AsNoTracking().Where(x => x.SiteName == host || x.SiteAlias == host)
         .Select(x => new SiteDto
         {
            SiteId = x.SiteId,
            SiteGuid =  x.SiteGuid
         })
         .FirstOrDefaultAsync(ct)) ?? await db.Sites.AsNoTracking().Where(x => x.SiteId == 1).Select(x => new SiteDto
      {
         SiteId = x.SiteId,
         SiteGuid =  x.SiteGuid
      }).FirstOrDefaultAsync(ct);

      return _resolvedSite ?? throw new Exception("No sites configured in database.");
   }
}