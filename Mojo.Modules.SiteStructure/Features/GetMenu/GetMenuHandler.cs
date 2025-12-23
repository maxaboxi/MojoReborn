using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mojo.Modules.SiteStructure.Data;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.SiteStructure.Features.GetMenu;

public class GetMenuHandler
{
    public static async Task<List<PageDto>> Handle(
        GetMenuQuery query, 
        SiteStructureDbContext db,
        ISiteResolver siteResolver,
        IUserService userService,
        ClaimsPrincipal claimsPrincipal,
        IConfiguration configuration,
        CancellationToken ct)
    {
        var roleName = configuration["Authentication:AllUsersRoleName"];
        
        if (roleName == null)
        {
            throw new Exception("AllUsersRoleName is not configured");
        }
        
        var site = await siteResolver.GetSite(ct);
        
        var user = await userService.GetUserAsync(claimsPrincipal, ct);

        var queryable = db.Pages.AsNoTracking().Where(p => p.SiteId == site.SiteId && p.IncludeInMenu == true);
        var roles = new [] { roleName };
        if (user != null)
        {
            var authenticatedRoleName = configuration["Authentication:AuthenticatedUsersRoleName"];

            if (authenticatedRoleName == null)
            {
                throw new Exception("AuthenticatedUsersRoleName is not configured");
            }
            
            roles = roles.Concat([authenticatedRoleName]).Concat(user.UserSiteRoles.Select(x => x.Name)).ToArray();
        }
        
        roles = roles.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();

        queryable = queryable.Where(p =>
            roles.Any(r => 
                EF.Functions.Like(";" + (p.AuthorizedRoles ?? "") + ";", "%;" + r + ";%") ||
                EF.Functions.Like(";" + (p.EditRoles ?? "") + ";", "%;" + r + ";%")));
        
        var rawPages = await queryable
            .OrderBy(p => p.PageOrder)
            .Select(p => new PageDto
            {
                Id = p.PageId,
                ParentId = p.ParentId,
                Title = p.PageName,
                FeatureName = p.PageModules.Where(pm => pm.PageId == p.PageId).Select(pm => pm.Module.ModuleDefinition.FeatureName).FirstOrDefault() ?? "Feature Name Missing",
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