using Mojo.Shared.Dtos.Identity;
using Mojo.Shared.Dtos.SiteStructure;

namespace Mojo.Shared.Interfaces.Identity;

public interface IPermissionService
{
    bool CanEdit(ApplicationUserDto user, FeatureContextDto context);
    bool HasAdministratorRights(ApplicationUserDto user, FeatureContextDto context, string roleToCheck);
}