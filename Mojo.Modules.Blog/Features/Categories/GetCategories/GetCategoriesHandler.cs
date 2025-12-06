using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;

namespace Mojo.Modules.Blog.Features.Categories.GetCategories;

public class GetCategoriesHandler
{
    public static async Task<List<GetCategoriesResponse>> Handle(
        GetCategoriesQuery query,
        BlogDbContext db,
        CancellationToken ct)
    {
        return await db.Categories.Select(x => new GetCategoriesResponse
            { Id = x.Id, CategoryName = x.CategoryName, ModuleId = x.ModuleId }).ToListAsync(ct);
    }
}