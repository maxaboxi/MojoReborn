using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Blog.Domain.Entities;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Posts.EditPost;

public static class EditPostHandler
{
    public static async Task<EditPostResponse> Handle(
        EditPostCommand command, 
        BlogDbContext db,
        IUserService userService,
        IHttpContextAccessor httpContextAccessor,
        IFeatureContextResolver featureContextResolver,
        IPermissionService permissionService,
        CancellationToken ct)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        var user = await userService.GetUserAsync(claimsPrincipal, ct) 
                   ?? throw new UnauthorizedAccessException();

        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, FeatureNames.Blog, ct)
                                ?? throw new KeyNotFoundException();
        
        if (!permissionService.CanEdit(user, featureContextDto))
        {
            throw new UnauthorizedAccessException();
        }
        
        var original = await db.BlogPosts
            .Include(b => b.Categories)
            .FirstOrDefaultAsync(x => 
                x.ModuleId == featureContextDto.ModuleId &&
                x.BlogPostId == command.BlogPostId, ct) ?? throw new KeyNotFoundException();

        if (original.Author != user.Email || !permissionService.CanEdit(user, featureContextDto))
        {
            throw new UnauthorizedAccessException();
        }
        
        original.Content = command.Content;
        original.Title = command.Title;
        original.SubTitle = command.SubTitle;
        original.ModifiedAt = DateTime.UtcNow;
        original.LastModifiedBy = user.Id;
        
        var currentIds = original.Categories.Select(c => c.Id).ToHashSet();
        var incomingIds = command.Categories.Select(c => c.Id).ToHashSet();
        var toRemove = original.Categories.Where(c => !incomingIds.Contains(c.Id)).ToList();
        
        foreach (var r in toRemove)
        {
            original.Categories.Remove(r);
        }
        
        var potentiallyNew = command.Categories.Where(c => !currentIds.Contains(c.Id)).ToList();

        if (potentiallyNew.Count > 0)
        {
            var namesToCheck = potentiallyNew.Select(c => c.CategoryName).Distinct().ToList();
            var idsToCheck = potentiallyNew.Where(c => c.Id > 0).Select(c => c.Id).ToList();
            
            var existingCategoriesInDb = await db.Categories
                .Where(c => c.ModuleId == original.ModuleId && 
                            (namesToCheck.Contains(c.CategoryName) || idsToCheck.Contains(c.Id)))
                .ToListAsync(ct);

            foreach (var dto in potentiallyNew)
            {
                var existing = existingCategoriesInDb.FirstOrDefault(c =>
                    (dto.Id > 0 && c.Id == dto.Id) ||
                    string.Equals(c.CategoryName, dto.CategoryName, StringComparison.CurrentCultureIgnoreCase)
                );

                if (existing != null)
                {
                    original.Categories.Add(existing);
                    continue;
                }
                
                original.Categories.Add(new BlogCategory { CategoryName = dto.CategoryName, ModuleId = original.ModuleId });
            }
        }
        
        await db.SaveChangesAsync(ct);
        
        return new EditPostResponse(original.BlogPostId);
    }
}