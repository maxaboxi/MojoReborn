using FluentValidation;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Posts.GetPost;

public record GetPostQuery(int PageId, Guid BlogPostId, DateTime? LastCommentDate, int? Amount)
{
    public string Name => FeatureNames.Blog;

    public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
    {
        public GetPostQueryValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.BlogPostId).NotNull().WithMessage("BlogPostId cannot be empty.");
            RuleFor(x => x.Amount).GreaterThan(0).LessThanOrEqualTo(100).When(x => x.Amount.HasValue);
        }
    }
}