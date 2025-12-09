using FluentValidation;

namespace Mojo.Modules.Blog.Features.Comments.CreateComment;

public class CreateBlogCommentCommand(int pageId, Guid blogPostId, string author, string title, string content)
{
    public int PageId { get; set; } = pageId;
    public Guid BlogPostId { get; set; } = blogPostId;
    public string Author { get; set; } = author;
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
    public string? UserIpAddress { get; set; }
    
    public class CreateBlogCommentValidator : AbstractValidator<CreateBlogCommentCommand>
    {
        public CreateBlogCommentValidator()
        {
            RuleFor(x => x.PageId).NotNull();
            RuleFor(x => x.BlogPostId).NotNull();
            RuleFor(x => x.Author).NotNull();
            RuleFor(x => x.Title).NotNull().MaximumLength(255);
            RuleFor(x => x.Content).NotNull();
        }
    }
}