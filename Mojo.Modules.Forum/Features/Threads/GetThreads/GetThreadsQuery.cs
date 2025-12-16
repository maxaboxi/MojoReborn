using FluentValidation;

namespace Mojo.Modules.Forum.Features.Threads.GetThreads;

public record GetThreadsQuery(int PageId, DateTime? LastThreadDate, int? LastThreadId, int? Amount)
{
    public class GetThreadsQueryValidator : AbstractValidator<GetThreadsQuery>
    {
        public GetThreadsQueryValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
        }
    }
}