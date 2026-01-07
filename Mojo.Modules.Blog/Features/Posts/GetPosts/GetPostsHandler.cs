using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Posts.GetPosts;

public static class GetPostsHandler
{
    public static async Task<GetPostsResponse> Handle(
        GetPostsQuery query, 
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        CancellationToken ct)
    {
        var featureContextDto = await featureContextResolver.ResolveModule(query.PageId, query.Name, ct)
                                ?? throw new KeyNotFoundException();
        
        var queryable = db.BlogPosts.AsNoTracking()
            .Where(x => x.ModuleId == featureContextDto.ModuleId);
        
        if (query is { LastPostDate: not null, LastPostId: not null })
        {
            queryable = queryable.Where(x => 
                x.CreatedAt < query.LastPostDate.Value ||
                (x.CreatedAt == query.LastPostDate.Value && x.Id < query.LastPostId.Value)
            );
        }
        
        var posts = await queryable
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Take(query.Amount ?? 20)
            .Select(p => new BlogPostDto
                (
                    p.Id,
                    p.BlogPostId,
                    p.Title,
                    p.Content.Length > 200 ? p.Content.Substring(0, 200) : p.Content, 
                    p.Author,
                    p.CreatedAt,
                    p.Categories.Select(c => c.CategoryName).ToList(),
                    p.Comments.Count 
                ))
            .ToListAsync(ct);

        return new GetPostsResponse(posts);
    }
}