using FluentValidation;

namespace Mojo.Modules.Blog.Features.Comments.EditComment;

public record EditBlogCommentCommand(int PageId, Guid BlogPostId, Guid BlogCommentId, string Title, string Content, string? ModerationReason)
{
    public class CreateBlogCommentValidator : AbstractValidator<EditBlogCommentCommand>
    {
        public CreateBlogCommentValidator()
        {
            RuleFor(x => x.BlogPostId).NotNull().WithMessage("BlogPostId cannot be empty");
            RuleFor(x => x.BlogCommentId).NotNull().WithMessage("BlogCommentId cannot be empty");
            RuleFor(x => x.Title).NotNull().WithMessage("Title cannot be empty")
                .MaximumLength(255).WithMessage("Title cannot be longer than 255 characters");
            RuleFor(x => x.Content).NotNull().WithMessage("Content cannot be empty");
        }
    }
}