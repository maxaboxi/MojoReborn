using FluentValidation;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Forum.Features.Threads.EditThread;

public record EditThreadCommand(int PageId, int ForumId, int ThreadId, string Subject) : IFeatureRequest
{
    public string Name => FeatureNames.Forum;
    public bool RequiresEditPermission => true;
    public bool UserCanEdit => true;

    public class EditThreadCommandValidator : AbstractValidator<EditThreadCommand>
    {
        public EditThreadCommandValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.ForumId).GreaterThan(0).WithMessage("ForumId must be greater than zero.");
            RuleFor(x => x.ThreadId).GreaterThan(0).WithMessage("ThreadId must be greater than zero.");
            RuleFor(x => x.Subject).NotNull().NotEmpty().WithMessage("Subject cannot be empty.");
        }
    }
}