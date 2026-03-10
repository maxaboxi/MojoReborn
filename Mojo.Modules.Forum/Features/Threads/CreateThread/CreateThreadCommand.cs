using FluentValidation;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Forum.Features.Threads.CreateThread;

public record CreateThreadCommand(int PageId, int ForumId, string Subject) : IFeatureRequest
{
    public string Name => FeatureNames.Forum;
    public bool RequiresEditPermission => true;
    public bool UserCanEdit => false;

    public class CreateThreadCommandValidator : AbstractValidator<CreateThreadCommand>
    {
        public CreateThreadCommandValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.ForumId).GreaterThan(0).WithMessage("ForumId must be greater than zero.");
            RuleFor(x => x.Subject).NotNull().NotEmpty().WithMessage("Subject cannot be empty.");
        }
    }
}