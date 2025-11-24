using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Features.Blog;

namespace Mojo.Modules.Blog.Features.GetPosts;

public record GetPostsQuery;

public static class GetPostsHandler
{
    public static async Task<List<BlogPostDto>> Handle(
        GetPostsQuery query, 
        BlogDbContext db, 
        CancellationToken ct)
    {
        return await db.BlogPosts
            .AsNoTracking()
            .Include(p => p.Categories)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new BlogPostDto
            {
                BlogPostGuid = p.BlogPostId,
                Title = p.Title,
                Content = p.Content.Substring(0, 200), 
                Author = p.Author,
                CreatedAt = p.CreatedAt,
                Categories = p.Categories.Select(c => c.CategoryName).ToList(),
                CommentCount = p.Comments.Count() 
            })
            .ToListAsync(ct);
    }
}