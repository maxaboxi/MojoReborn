using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.SiteStructure;

namespace Mojo.Modules.Blog.Features.Posts.GetPost;

public static class GetPostHandler
{
    public static async Task<GetPostResponse> Handle(
        GetPostQuery query,
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        CancellationToken ct)
    {
        var featureContextDto = await featureContextResolver.ResolveModule(query.PageId, query.Name, ct)
                                ?? throw new KeyNotFoundException();
        
        var postResponse = await db.BlogPosts.AsNoTracking()
            .Where(x => x.BlogPostId == query.BlogPostId && x.ModuleId == featureContextDto.ModuleId)
            .Select(bp => new GetPostResponse
            (
                bp.BlogPostId,
                bp.Title,
                bp.SubTitle,
                bp.Content, 
                bp.Author,
                bp.CreatedAt,
                bp.Categories.Select(c => c.CategoryName).ToList(),
                bp.Comments.Count,
                bp.Comments
                    .Where(x => query.LastCommentDate == null || x.CreatedAt < query.LastCommentDate.Value)
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(query.Amount ?? 50)
                    .Select(bpc => 
                    new BlogCommentDto(
                        bpc.Id, 
                        bpc.UserGuid, 
                        bpc.UserName, 
                        bpc.Title, 
                        bpc.Content.Length > 200 ? bpc.Content.Substring(0, 200) : bpc.Content, 
                        bpc.CreatedAt, 
                        bpc.ModifiedAt, 
                        bpc.ModeratedBy, 
                        bpc.ModerationReason))
                    .ToList()
            ))
            .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException();

        return postResponse;
    }
}