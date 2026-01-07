using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Categories.DeleteCategory;

public class DeleteCategoryHandler
{
    public static async Task<DeleteCategoryResponse> Handle(
        DeleteCategoryCommand command,
        BlogDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
        var existingCategoryInDb = await db.Categories
            .Where(c => c.ModuleId == securityContext.FeatureContext.ModuleId)
            .Where(c => c.Id == command.CategoryId)
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();

        db.Categories.Remove(existingCategoryInDb);
        await db.SaveChangesAsync(ct);

        return new DeleteCategoryResponse();
    }
}