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
        
        return await db.BlogPosts.Where(x => x.BlogPostId == query.BlogPostId)
            .AsNoTracking()
            .Where(x => x.ModuleId == featureContextDto.ModuleId)
            .Include(x => x.Categories)
            .Include(x => x.Comments)
            .Select(bp => new GetPostResponse()
            {
                BlogPostGuid =  bp.BlogPostId,
                Title = bp.Title,
                SubTitle = bp.SubTitle,
                Content = bp.Content, 
                Author = bp.Author,
                CreatedAt = bp.CreatedAt,
                Categories = bp.Categories.Select(c => c.CategoryName).ToList(),
                CommentCount = bp.Comments.Count(),
                Comments = bp.Comments.Select(bpc => 
                    new BlogCommentDto(
                        bpc.Id, 
                        bpc.UserGuid, 
                        bpc.UserName, 
                        bpc.Title, 
                        bpc.Content.Substring(0, 200), 
                        bpc.CreatedAt, 
                        bpc.ModifiedAt, 
                        bpc.ModeratedBy, 
                        bpc.ModerationReason))
                    .ToList(),
                
            })
            .FirstAsync(ct);
    }
}