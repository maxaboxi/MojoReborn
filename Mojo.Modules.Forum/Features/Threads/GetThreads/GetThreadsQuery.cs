using FluentValidation;

namespace Mojo.Modules.Forum.Features.Threads.GetThreads;

public record GetThreadsQuery(int PageId, int ForumId)
{
    public class GetThreadsQueryValidator : AbstractValidator<GetThreadsQuery>
    {
        public GetThreadsQueryValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.ForumId).NotNull().WithMessage("ForumId cannot be empty.");
        }
    }
}