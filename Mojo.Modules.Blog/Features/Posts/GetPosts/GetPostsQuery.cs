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
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
        }
    }
}