using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mojo.Modules.SiteStructure.Data;
using Mojo.Shared.Dtos.SiteStructure;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.SiteStructure.Features.GetSite;

public class SiteResolver(SiteStructureDbContext db, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : ISiteResolver
{
   private SiteDto? _resolvedSite;
   
   public async Task<SiteDto> GetSite(CancellationToken ct)
   {
      if (_resolvedSite != null)
      {
         return _resolvedSite;
      }

      var forcedSiteId = configuration["ForceSiteId"];

      if (!string.IsNullOrEmpty(forcedSiteId))
      {
         _resolvedSite = await db.Sites.AsNoTracking().Where(x => x.SiteId == int.Parse(forcedSiteId)).Select(x => new SiteDto
         {
            SiteId = x.SiteId,
            SiteGuid =  x.SiteGuid
         }).FirstOrDefaultAsync(ct);
         
         return _resolvedSite ?? throw new Exception($"No sites configured in database with the forced site id: {forcedSiteId}.");
      }
      
      var context = httpContextAccessor.HttpContext;

      if (context == null)
      {
         throw new InvalidOperationException("SiteResolver cannot be used outside of an HTTP request.");
      }
      
      var host = context.Request.Host.Host;

      _resolvedSite = await db.SiteHosts.AsNoTracking().Where(x => x.HostName == host)
         .Select(x => new SiteDto
         {
            SiteId = x.SiteId,
            SiteGuid = x.SiteGuid
         })
         .FirstOrDefaultAsync(ct);

      if (_resolvedSite == null)
      {
         _resolvedSite = await db.Sites.AsNoTracking().Where(x => x.SiteName == host || x.SiteAlias == host)
            .Select(x => new SiteDto
            {
               SiteId = x.SiteId,
               SiteGuid = x.SiteGuid
            })
            .FirstOrDefaultAsync(ct);
      }

      if (_resolvedSite == null)
      {
         _resolvedSite = await db.Sites.AsNoTracking().Where(x => x.SiteId == 1).Select(x => new SiteDto
         {
            SiteId = x.SiteId,
            SiteGuid =  x.SiteGuid
         }).FirstOrDefaultAsync(ct);
      }

      return _resolvedSite ?? throw new Exception("No sites configured in database.");
   }
}