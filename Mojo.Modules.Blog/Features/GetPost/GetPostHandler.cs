using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Features.Blog;

namespace Mojo.Modules.Blog.Features.GetPost;

public record GetPostQuery(Guid blogPostId);

public static class GetPostHandler
{
    public static async Task<BlogPostDto> Handle(
        GetPostQuery query,
        BlogDbContext db,
        CancellationToken ct)
    {
        return await db.BlogPosts.Where(x => x.BlogPostId == query.blogPostId)
            .AsNoTracking()
            .Include(x => x.Categories)
            .Include(x => x.Comments)
            .Select(bp => new BlogPostDto()
            {
                BlogPostGuid =  bp.BlogPostId,
                Title = bp.Title,
                Content = bp.Content, 
                Author = bp.Author,
                CreatedAt = bp.CreatedAt,
                Categories = bp.Categories.Select(c => c.CategoryName).ToList(),
                CommentCount = bp.Comments.Count(),
                Comments = bp.Comments.Select(bpc => new BlogCommentDto()
                {
                    Guid = bpc.Guid,
                    UserGuid = bpc.UserGuid,
                    Title = bpc.Title,
                    Content = bpc.Content.Substring(0, 200),
                    UserName = bpc.UserName,
                    CreatedAt = bpc.CreatedAt,
                    ModifiedAt = bpc.ModifiedAt,
                    ModeratedBy = bpc.ModeratedBy,
                    ModerationReason = bpc.ModerationReason
                }).ToList(),
                
            })
            .FirstAsync(ct);
    }
}