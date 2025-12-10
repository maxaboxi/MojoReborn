using FluentValidation;

namespace Mojo.Modules.Blog.Features.Posts.EditPost;

public record EditPostCommand(int PageId, Guid BlogPostId, string Title, string SubTitle, string Content, List<EditPostCategoryDto> Categories)
{
    public class EditPostValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.BlogPostId).NotNull().WithMessage("BlogPostId cannot be empty.");
            RuleFor(x => x.Title).NotNull().WithMessage("Title cannot be empty.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");
            RuleFor(x => x.SubTitle).NotNull().WithMessage("SubTitle cannot be empty.")
                .MaximumLength(500).WithMessage("SubTitle cannot exceed 500 characters.");
            RuleFor(x => x.Content).NotNull().WithMessage("Content cannot be empty.");
            RuleFor(x => x.Categories).NotNull().NotEmpty().WithMessage("Categories cannot be empty.");
        }
    }
}