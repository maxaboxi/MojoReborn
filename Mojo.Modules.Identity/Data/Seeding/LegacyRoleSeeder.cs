using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mojo.Modules.Identity.Domain.Entities;

namespace Mojo.Modules.Identity.Data.Seeding;

public class LegacyRoleSeeder
{
    public static async Task SeedAsync(IdentityDbContext db, ILogger logger)
    {
        var legacyRoles = await db.LegacyRoles.Where(x => x.RoleGuid != null).ToListAsync();
        
        var existingSiteRoleIds = await db.SiteRoles.Select(x => x.Id).ToListAsync();
        var existingSiteRoleIdsSet = new HashSet<Guid>(existingSiteRoleIds);
        
        var newRoles = new List<SiteRole>();

        foreach (var role in legacyRoles)
        {
            if (role.RoleGuid.HasValue && !existingSiteRoleIdsSet.Contains(role.RoleGuid.Value))
            {
                newRoles.Add(new SiteRole
                {
                    Id =  role.RoleGuid.Value,
                    SiteId = role.SiteId,
                    SiteGuid =  role.SiteGuid,
                    Name = role.RoleName,
                    DisplayName = role.DisplayName ?? "",
                    Description = role.Description ?? ""
                });
            }
        }

        if (newRoles.Count != 0)
        {
            logger.LogInformation($"Migrating {newRoles.Count} legacy roles to SiteRoles...");
            await db.SiteRoles.AddRangeAsync(newRoles);
            await db.SaveChangesAsync();
        }
    }
}