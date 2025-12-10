using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Categories.GetCategories;

public class GetCategoriesHandler
{
    public static async Task<GetCategoriesResponse> Handle(
        GetCategoriesQuery query,
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        CancellationToken ct)
    {
        var featureContextDto = await featureContextResolver.ResolveModule(query.PageId, "BlogFeatureName", ct);
        
        if (featureContextDto == null)
        {
            return BaseResponse.NotFound<GetCategoriesResponse>("Module not found.");
        }

        var categories = await db.Categories
            .AsNoTracking()
            .Where(x => x.ModuleId == featureContextDto.ModuleId)
            .Select(x => new CategoryDto(x.Id, x.ModuleId, x.CategoryName))
            .ToListAsync(ct);
        return new GetCategoriesResponse { IsSuccess = true, Categories = categories };
    }
}