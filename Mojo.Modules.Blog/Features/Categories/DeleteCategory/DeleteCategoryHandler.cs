using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Categories.DeleteCategory;

public class DeleteCategoryHandler
{
    public static async Task<DeleteCategoryResponse> Handle(
        DeleteCategoryCommand command,
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct) 
                   ?? throw new UnauthorizedAccessException();

        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, command.Name, ct)
                                ?? throw new KeyNotFoundException();
        
        if (!permissionService.CanEdit(user, featureContextDto))
        {
            throw new UnauthorizedAccessException();
        }

        var existingCategoryInDb = await db.Categories
            .Where(c => c.ModuleId == featureContextDto.ModuleId)
            .Where(c => c.Id == command.CategoryId)
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();

        db.Categories.Remove(existingCategoryInDb);
        await db.SaveChangesAsync(ct);

        return new DeleteCategoryResponse();
    }
}