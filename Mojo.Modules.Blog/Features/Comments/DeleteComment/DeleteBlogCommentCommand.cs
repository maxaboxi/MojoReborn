using FluentValidation;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Comments.DeleteComment;

public record DeleteBlogCommentCommand(int PageId, Guid BlogPostId, Guid BlogPostCommentId)
{
    public string Name => FeatureNames.Blog;

    public class DeleteBlogCommentCommandValidator : AbstractValidator<DeleteBlogCommentCommand>
    {
        public DeleteBlogCommentCommandValidator()
        {
            RuleFor(x => x.PageId).NotEmpty().WithMessage("PageId cannot be empty");
            RuleFor(x => x.BlogPostId).NotEmpty().WithMessage("BlogPostId cannot be empty");
            RuleFor(x => x.BlogPostCommentId).NotEmpty().WithMessage("BlogPostCommentId cannot be empty");
        }
    }
}