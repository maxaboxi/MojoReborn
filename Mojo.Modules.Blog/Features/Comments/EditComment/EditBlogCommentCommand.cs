using FluentValidation;

namespace Mojo.Modules.Blog.Features.Comments.EditComment;

public class EditBlogCommentCommand(int pageId, Guid blogPostId, Guid blogCommentId, string title, string content)
{
    public int PageId { get; set; } = pageId;
    public Guid BlogPostId { get; set; } = blogPostId;
    public Guid BlogCommentId { get; set; } = blogCommentId;
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
    
    public class CreateBlogCommentValidator : AbstractValidator<EditBlogCommentCommand>
    {
        public CreateBlogCommentValidator()
        {
            RuleFor(x => x.BlogPostId).NotNull();
            RuleFor(x => x.BlogCommentId).NotNull();
            RuleFor(x => x.Title).NotNull().MaximumLength(255);
            RuleFor(x => x.Content).NotNull();
        }
    }
}