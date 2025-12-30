using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Blog.Domain.Entities;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Categories.CreateCategory;

public class CreateCategoryHandler
{
    public static async Task<CreateCategoryResponse> Handle(
        CreateCategoryCommand command,
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct) 
                   ?? throw new UnauthorizedAccessException();

        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, FeatureNames.Blog, ct)
                                ?? throw new KeyNotFoundException();
        
        if (!permissionService.CanEdit(user, featureContextDto))
        {
            throw new UnauthorizedAccessException();
        }

        var existingCategoryInDb = await db.Categories
            .Where(c => c.ModuleId == featureContextDto.ModuleId)
            .Where(c => c.CategoryName == command.CategoryName)
            .FirstOrDefaultAsync(ct) ?? throw new InvalidOperationException("Category with the given name already exists.");
            
        await db.Categories.AddAsync(new BlogCategory { CategoryName = command.CategoryName, ModuleId = featureContextDto.ModuleId },
            ct);
        await db.SaveChangesAsync(ct);

        return new CreateCategoryResponse();
    }
}