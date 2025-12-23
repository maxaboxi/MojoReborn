using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Posts.GetPost;

public static class GetPostHandler
{
    public static async Task<GetPostResponse> Handle(
        GetPostQuery query,
        BlogDbContext db,
        IFeatureContextResolver featureContextResolver,
        CancellationToken ct)
    {
        var featureContextDto = await featureContextResolver.ResolveModule(query.PageId, "BlogFeatureName", ct);
        
        if (featureContextDto == null)
        {
            return BaseResponse.NotFound<GetPostResponse>("Module not found.");
        }
        
        var postResponse = await db.BlogPosts.AsNoTracking()
            .Where(x => x.BlogPostId == query.BlogPostId && x.ModuleId == featureContextDto.ModuleId)
            .Select(bp => new GetPostResponse
            {
                BlogPostGuid =  bp.BlogPostId,
                Title = bp.Title,
                SubTitle = bp.SubTitle,
                Content = bp.Content, 
                Author = bp.Author,
                CreatedAt = bp.CreatedAt,
                Categories = bp.Categories.Select(c => c.CategoryName).ToList(),
                CommentCount = bp.Comments.Count,
                Comments = bp.Comments
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
                    .ToList(),
                
            })
            .FirstOrDefaultAsync(ct);
        
        return postResponse ?? BaseResponse.NotFound<GetPostResponse>("Post not found.");
    }
}