using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Identity.Domain.Entities;
using Mojo.Shared.Dtos.Identity;
using Mojo.Shared.Interfaces.Identity;

namespace Mojo.Modules.Identity.Features.Services;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<ApplicationUserDto?> GetUserAsync(ClaimsPrincipal principal, CancellationToken ct = default)
    {
        var userId = userManager.GetUserId(principal);
        if (userId == null)
        {
            return null;
        }
        
        var user = await userManager.Users.AsNoTracking()
            .Include(u => u.UserSiteProfiles)
            .Include(u => u.UserSiteRoleAssignments)
                .ThenInclude(us => us.Role)
            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId), ct);

        if (user == null)
        {
            return null;
        }

        return new ApplicationUserDto(
            user.Id,
            user.Email ?? "",
            user.FirstName,
            user.LastName,
            user.DisplayName,
            user.Bio,
            user.Signature,
            user.LegacyId,
            user.AvatarUrl,
            user.TimeZoneId,
            user.UserSiteProfiles.Select(x => new UserSiteProfileDto(x.UserId, x.SiteId, x.SiteGuid)).ToList(),
            user.UserSiteRoleAssignments.Select(x =>
                new UserSiteRoleDto(x.Role.Id, x.Role.SiteId, x.Role.SiteGuid, x.Role.Name, x.Role.DisplayName,
                    x.Role.Description)).ToList());
    }
}