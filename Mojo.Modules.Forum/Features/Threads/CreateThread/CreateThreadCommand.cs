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
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.ForumId).NotNull().WithMessage("ForumId cannot be empty.");
            RuleFor(x => x.Subject).NotNull().NotEmpty().WithMessage("Subject cannot be empty.");
        }
    }
}