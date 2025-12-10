using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Core.Data;
using Mojo.Modules.Core.Features.SiteStructure.GetSite;

namespace Mojo.Modules.Core.Features.SiteStructure.GetMenu;

public class GetMenuHandler
{
    public static async Task<List<PageDto>> Handle(
        GetMenuQuery query, 
        CoreDbContext db,
        SiteResolver siteResolver,
        CancellationToken ct)
    {
        var site = await siteResolver.GetSite(ct);
        
        var rawPages = await db.Pages
            .AsNoTracking()
            .Where(p => p.SiteId == site.SiteId && p.IncludeInMenu == true)
            .Include(p => p.PageModules)
                .ThenInclude(p => p.Module)
            .OrderBy(p => p.PageOrder)
            .Select(p => new PageDto
            {
                Id = p.PageId,
                ParentId = p.ParentId,
                Title = p.PageName,
                ModuleTitle = p.PageModules.First(x => x.PageId == p.PageId).Module.Title,
                Url = p.Url.Replace("~/", "/"), // Fix legacy ASP.NET paths
                ViewRoles = p.AuthorizedRoles,
                Order = p.PageOrder
            })
            .ToListAsync(ct);

        // Build the Nav Tree
        var lookup = rawPages.ToDictionary(x => x.Id);
        var rootNodes = new List<PageDto>();

        foreach (var page in rawPages)
        {
            // If it has a parent AND that parent exists in our list...
            if (page.ParentId.HasValue && lookup.TryGetValue(page.ParentId.Value, out var parent))
            {
                parent.Children.Add(page);
            }
            else
            {
                // Otherwise, it's a root item
                rootNodes.Add(page);
            }
        }

        return rootNodes;
    }
}