using FluentValidation;

namespace Mojo.Modules.Forum.Features.Threads.GetThread;

public record GetThreadQuery(int PageId, int ForumId, int ThreadId, int? Amount, int LastThreadSequence = 0)
{
    public class GetThreadQueryValidator : AbstractValidator<GetThreadQuery>
    {
        public GetThreadQueryValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.ForumId).NotNull().WithMessage("ForumId cannot be empty.");
            RuleFor(x => x.ThreadId).NotNull().WithMessage("ThreadId cannot be empty.");
        }
    }
}