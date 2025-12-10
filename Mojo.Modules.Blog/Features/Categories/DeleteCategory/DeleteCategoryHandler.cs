using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Categories.DeleteCategory;

public class DeleteCategoryHandler
{
    public static async Task<DeleteCategoryResponse> Handle(
        DeleteCategoryCommand command,
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var user = userService.GetUserAsync(claimsPrincipal, ct).Result;
        
        if (user == null)
        {
            return BaseResponse.Unauthorized<DeleteCategoryResponse>("User not found.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "BlogFeatureName", ct);
        
        if (featureContextDto == null || !permissionService.CanEdit(user, featureContextDto))
        {
            return BaseResponse.Unauthorized<DeleteCategoryResponse>();
        }

        var existingCategoryInDb = await db.Categories
            .Where(c => c.ModuleId == featureContextDto.ModuleId)
            .Where(c => c.Id == command.CategoryId)
            .FirstOrDefaultAsync(ct);

        if (existingCategoryInDb == null)
        {
            return BaseResponse.NotFound<DeleteCategoryResponse>("Category not found.");
        }

        db.Categories.Remove(existingCategoryInDb);
        await db.SaveChangesAsync(ct);

        return BaseResponse.Success<DeleteCategoryResponse>();
    }
}