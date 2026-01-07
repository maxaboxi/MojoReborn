using System.Security.Claims;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.Security;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Web.Middleware;

public class FeatureSecurityMiddleware
{
    public static async Task<SecurityContext> LoadAsync(
        IFeatureRequest featureRequest,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct) 
                   ?? throw new UnauthorizedAccessException();

        var featureContextDto = await featureContextResolver.ResolveModule(featureRequest.PageId, featureRequest.Name, ct)
                                ?? throw new KeyNotFoundException();

        var isAdmin = permissionService.HasAdministratorRightsToThePage(user, featureContextDto);

        if (!featureRequest.RequiresEditPermission || permissionService.CanEdit(user, featureContextDto) ||
            featureRequest.UserCanEdit)
        {
            return new SecurityContext(user, featureContextDto, isAdmin);
        }
        
        throw new UnauthorizedAccessException();
    }
}