using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Blog.Domain.Entities;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Categories.CreateCategory;

public class CreateCategoryHandler
{
    public static async Task<CreateCategoryResponse> Handle(
        CreateCategoryCommand command,
        BlogDbContext db,
        SecurityContext securityContext,
        CancellationToken ct)
    {
       var existingCategoryInDb = await db.Categories
            .Where(c => c.ModuleId == securityContext.FeatureContext.ModuleId)
            .Where(c => c.CategoryName == command.CategoryName)
            .FirstOrDefaultAsync(ct);

        if (existingCategoryInDb != null)
        {
            throw new InvalidOperationException("Category with the given name already exists.");
        }
            
        await db.Categories.AddAsync(new BlogCategory { CategoryName = command.CategoryName, ModuleId = securityContext.FeatureContext.ModuleId },
            ct);
        await db.SaveChangesAsync(ct);

        return new CreateCategoryResponse();
    }
}