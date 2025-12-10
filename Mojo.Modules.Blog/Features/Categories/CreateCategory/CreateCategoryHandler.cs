using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Blog.Domain.Entities;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

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
        var user = userService.GetUserAsync(claimsPrincipal, ct).Result;
        
        if (user == null)
        {
            return BaseResponse.Unauthorized<CreateCategoryResponse>("User not found.");
        }
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "BlogFeatureName", ct);
        
        if (featureContextDto == null || !permissionService.CanEdit(user, featureContextDto))
        {
            return BaseResponse.Unauthorized<CreateCategoryResponse>();
        }

        var existingCategoryInDb = await db.Categories
            .Where(c => c.ModuleId == featureContextDto.ModuleId)
            .Where(c => c.CategoryName == command.CategoryName)
            .FirstOrDefaultAsync(ct);

        if (existingCategoryInDb != null)
        {
            return new CreateCategoryResponse { IsSuccess = false, Message = "Category already exists." };
        }
            
        await db.Categories.AddAsync(new BlogCategory { CategoryName = command.CategoryName, ModuleId = featureContextDto.ModuleId },
            ct);
        await db.SaveChangesAsync(ct);

        return BaseResponse.Success<CreateCategoryResponse>();

    }
}