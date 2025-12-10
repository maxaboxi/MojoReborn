using FluentValidation;

namespace Mojo.Modules.Blog.Features.Posts.GetPosts;

public record GetPostsQuery(int PageId)
{
    public class GetPostsQueryValidator : AbstractValidator<GetPostsQuery>
    {
        public GetPostsQueryValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
        }
    }
}