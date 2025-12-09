using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Posts.GetPosts;

public static class GetPostsHandler
{
    public static async Task<GetPostsResponse> Handle(
        GetPostsQuery query, 
        BlogDbContext db,
        IModuleResolver moduleResolver,
        CancellationToken ct)
    {
        var moduleDto = await moduleResolver.ResolveModule(query.PageId, "BlogFeatureName", ct);
        
        if (moduleDto == null)
        {
            return BaseResponse.NotFound<GetPostsResponse>("Module not found.");
        }
        var posts = await db.BlogPosts
            .AsNoTracking()
            .Include(p => p.Categories)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new BlogPostDto
            (
                p.BlogPostId,
                p.Title,
                p.Content.Substring(0, 200), 
                p.Author,
                p.CreatedAt,
                p.Categories.Select(c => c.CategoryName).ToList(),
                p.Comments.Count() 
            ))
            .ToListAsync(ct);
        
        return new GetPostsResponse { IsSuccess = true, BlogPosts = posts };
    }
}