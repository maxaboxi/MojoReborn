using FluentValidation;

namespace Mojo.Modules.Blog.Features.Posts.CreatePost;

public record CreatePostCommand(int PageId, string Title, string SubTitle, string Content, List<CreatePostCategoryDto> Categories)
{
    public class CreatePostValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.Title).NotNull().WithMessage("Title cannot be empty.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");
            RuleFor(x => x.SubTitle).NotNull().WithMessage("SubTitle cannot be empty.")
                .MaximumLength(500).WithMessage("SubTitle cannot exceed 500 characters.");
            RuleFor(x => x.Content).NotNull().WithMessage("Content cannot be empty.");
            RuleFor(x => x.Categories).NotNull().NotEmpty().WithMessage("Categories cannot be empty.");
        }
    }
}