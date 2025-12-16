using FluentValidation;

namespace Mojo.Modules.Blog.Features.Posts.GetPost;

public record GetPostQuery(int PageId, Guid BlogPostId, DateTime? LastCommentDate, int? Amount)
{
    public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
    {
        public GetPostQueryValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.BlogPostId).NotNull().WithMessage("BlogPostId cannot be empty.");
        }
    }
}