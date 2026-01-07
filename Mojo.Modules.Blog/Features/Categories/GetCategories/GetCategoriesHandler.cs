using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Categories.GetCategories;

public class GetCategoriesHandler
{
    public static async Task<GetCategoriesResponse> Handle(
        GetCategoriesQuery query,
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        CancellationToken ct)
    {
        var featureContextDto = await featureContextResolver.ResolveModule(query.PageId, query.Name, ct)
                                ?? throw new KeyNotFoundException();

        var categories = await db.Categories
            .AsNoTracking()
            .Where(x => x.ModuleId == featureContextDto.ModuleId)
            .Select(x => new CategoryDto(x.Id, x.ModuleId, x.CategoryName))
            .ToListAsync(ct);
        
        return new GetCategoriesResponse(categories);
    }
}