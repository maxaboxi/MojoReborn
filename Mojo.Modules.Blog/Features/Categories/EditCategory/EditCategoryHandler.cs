using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Categories.EditCategory;

public class EditCategoryHandler
{
    public static async Task<EditCategoryResponse> Handle(
        EditCategoryCommand command,
        BlogDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        var existingCategoryInDb = await db.Categories
            .Where(c => c.ModuleId == securityContext.FeatureContext.ModuleId)
            .Where(c => c.Id == command.CategoryId)
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();
        
        existingCategoryInDb.CategoryName = command.CategoryName;

        await db.SaveChangesAsync(ct);

        return new EditCategoryResponse();
    }
}