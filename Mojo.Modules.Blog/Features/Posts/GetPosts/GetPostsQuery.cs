using FluentValidation;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Posts.GetPosts;

public record GetPostsQuery(int PageId, DateTime? LastPostDate, int? LastPostId, int? Amount)
{
    public string Name => FeatureNames.Blog;

    public class GetPostsQueryValidator : AbstractValidator<GetPostsQuery>
    {
        public GetPostsQueryValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.Amount).GreaterThan(0).LessThanOrEqualTo(100).When(x => x.Amount.HasValue);
        }
    }
}