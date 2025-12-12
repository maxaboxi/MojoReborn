using Mojo.Shared.Dtos.Identity;
using Mojo.Shared.Dtos.SiteStructure;
using Mojo.Shared.Interfaces.Identity;

namespace Mojo.Modules.Identity.Features.Services;

public class PermissionService : IPermissionService
{
    public bool CanEdit(ApplicationUserDto user, FeatureContextDto featureContextDto)
    {
        if (user.UserSiteRoles.FirstOrDefault(x => x.SiteId == featureContextDto.SiteId) == null)
        {
            return false;
        }

        if (featureContextDto.PageDto.EditRoles.Contains("All Users") ||
            string.IsNullOrWhiteSpace(featureContextDto.PageDto.EditRoles))
        {
            return true;
        }
        
        var userRoles = user.UserSiteRoles.Select(x => x.Name).ToList();

        if (userRoles.Contains("Admins"))
        {
            return true;
        }

        var editRoles = featureContextDto.PageDto.EditRoles
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim());
        
        var hasAccess = editRoles.Intersect(userRoles).Any();

        return hasAccess;
    }
}