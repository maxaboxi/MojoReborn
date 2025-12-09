using FluentValidation;

namespace Mojo.Modules.Blog.Features.Posts.EditPost;

public class EditPostCommand(int pageId, Guid blogPostId, string title, string subtitle, string content, List<EditPostCategoryDto> categories)
{
    public int PageId { get; set; }
    public Guid BlogPostId { get; set; } = blogPostId;
    public string Title { get; set; } = title;
    public string SubTitle { get; set; } = subtitle;
    public string Content { get; set; } = content;
    public List<EditPostCategoryDto> Categories { get; set; } = categories;

    public class EditPostValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostValidator()
        {
            RuleFor(x => x.BlogPostId).NotNull();
            RuleFor(x => x.Title).NotNull().MaximumLength(255);
            RuleFor(x => x.SubTitle).NotNull().MaximumLength(500);
            RuleFor(x => x.Content).NotNull();
            RuleFor(x => x.Categories).NotNull().NotEmpty();
        }
    }
}