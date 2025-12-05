using FluentValidation;

namespace Mojo.Modules.Blog.Features.Comments.CreateComment;

public class CreateBlogCommentCommand(int pageId, Guid blogPostId, Guid userId, string userName, string userEmail, string title, string content)
{
    public int PageId { get; set; } = pageId;
    public Guid BlogPostId { get; set; } = blogPostId;
    public Guid UserId { get; set; } = userId;
    public string UserName { get; set; } = userName;
    public string UserEmail { get; set; } = userEmail;
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
    public string? UserIpAddress { get; set; }
    
    public class CreateBlogCommentValidator : AbstractValidator<CreateBlogCommentCommand>
    {
        public CreateBlogCommentValidator()
        {
            RuleFor(x => x.PageId).NotNull();
            RuleFor(x => x.BlogPostId).NotNull();
            RuleFor(x => x.Title).NotNull().MaximumLength(255);
            RuleFor(x => x.Content).NotNull();
        }
    }
}