using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Blog.Domain.Entities;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Comments.CreateComment;

public class CreateBlogCommentHandler
{
    public static async Task<CreateBlogCommentResponse> Handle(
        CreateBlogCommentCommand command,
        BlogDbContext db,
        ClaimsPrincipal claimsPrincipal,
        IUserService userService,
        IFeatureContextResolver featureContextResolver,
        CancellationToken ct)
    {
        var user = await userService.GetUserAsync(claimsPrincipal, ct);
        
        var featureContextDto = await featureContextResolver.ResolveModule(command.PageId, "BlogFeatureName", ct);
        
        if (featureContextDto == null)
        {
            return BaseResponse.NotFound<CreateBlogCommentResponse>();
        }

        var blogPost = await db.BlogPosts
            .Where(x => x.BlogPostId == command.BlogPostId && x.ModuleGuid == featureContextDto.ModuleGuid)
            .FirstOrDefaultAsync(ct);

        if (blogPost == null)
        {
            return BaseResponse.NotFound<CreateBlogCommentResponse>("BlogPost not found.");
        }

        var comment = new BlogComment
        {
            ModuleGuid = featureContextDto.ModuleGuid,
            SiteGuid = featureContextDto.SiteGuid,
            FeatureGuid =  featureContextDto.FeatureGuid,
            ContentGuid = blogPost.BlogPostId,
            UserGuid = user?.Id,
            UserEmail = user?.Email,
            UserName = user?.DisplayName ?? command.Author,
            UserIpAddress = command.UserIpAddress ?? "",
            Title = command.Title,
            Content = command.Content,
            CreatedAt =  DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            BlogPost = blogPost
        };
        
        await db.BlogComments.AddAsync(comment, ct);
        await db.SaveChangesAsync(ct);
        
        return new CreateBlogCommentResponse { IsSuccess = true, CommentId = comment.Id, Message = "Comment created successfully." }; 
    }
}