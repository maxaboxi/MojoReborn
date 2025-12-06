using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Core.Features.SiteStructure.GetModule;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Categories.GetCategories;

public class GetCategoriesHandler
{
    public static async Task<GetCategoriesResponse> Handle(
        GetCategoriesQuery query,
        BlogDbContext db,
        ModuleResolver moduleResolver,
        CancellationToken ct)
    {
        var moduleDto = await moduleResolver.GetModuleByPageId(query.PageId, ct);
        
        if (moduleDto == null)
        {
            return BaseResponse.NotFound<GetCategoriesResponse>("Module not found.");
        }

        var categories = await db.Categories.Select(x => new CategoryDto(x.Id, x.ModuleId, x.CategoryName)).ToListAsync(ct);
        return new GetCategoriesResponse { IsSuccess = true, Categories = categories };
    }
}