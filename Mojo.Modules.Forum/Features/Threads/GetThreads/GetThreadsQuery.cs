using FluentValidation;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Forum.Features.Threads.GetThreads;

public record GetThreadsQuery(int PageId, int? LastThreadSequence, int? Amount)
{
    public string Name => FeatureNames.Forum;

    public class GetThreadsQueryValidator : AbstractValidator<GetThreadsQuery>
    {
        public GetThreadsQueryValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.Amount).LessThanOrEqualTo(100).When(x => x.Amount.HasValue);
        }
    }
}