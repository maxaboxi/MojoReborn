using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Blog.Domain.Entities;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Comments.CreateComment;

public class CreateBlogCommentHandler
{
    public static async Task<CreateBlogCommentResponse> Handle(
        CreateBlogCommentCommand command,
        BlogDbContext db,
        IModuleResolver moduleResolver,
        CancellationToken ct)
    {
        var moduleDto = await moduleResolver.ResolveModule(command.PageId, "BlogFeatureName", ct);
        
        if (moduleDto == null)
        {
            return BaseResponse.NotFound<CreateBlogCommentResponse>("Module not found.");
        }

        var blogPost = await db.BlogPosts
            .Where(x => x.BlogPostId == command.BlogPostId)
            .FirstOrDefaultAsync(ct);

        if (blogPost == null)
        {
            return BaseResponse.NotFound<CreateBlogCommentResponse>("BlogPost not found.");
        }

        var comment = new BlogComment
        {
            ModuleGuid = moduleDto.ModuleGuid,
            SiteGuid = moduleDto.SiteGuid,
            FeatureGuid =  moduleDto.FeatureGuid,
            UserGuid = command.UserId,
            UserEmail = command.UserEmail,
            UserName = command.UserName,
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