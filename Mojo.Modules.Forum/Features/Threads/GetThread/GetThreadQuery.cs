using FluentValidation;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Forum.Features.Threads.GetThread;

public record GetThreadQuery(int PageId, int ForumId, int ThreadId, int? Amount, int LastThreadSequence = 0)
{
    public string Name => FeatureNames.Forum;

    public class GetThreadQueryValidator : AbstractValidator<GetThreadQuery>
    {
        public GetThreadQueryValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.ForumId).GreaterThan(0).WithMessage("ForumId must be greater than zero.");
            RuleFor(x => x.ThreadId).GreaterThan(0).WithMessage("ThreadId must be greater than zero.");
            RuleFor(x => x.Amount).GreaterThan(0).LessThanOrEqualTo(100).When(x => x.Amount.HasValue);
        }
    }
}