using FluentValidation;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Forum.Features.Threads.EditThread;

public record EditThreadCommand(int PageId, int ForumId, int ThreadId, string Subject)
{
    public string Name => FeatureNames.Forum;

    public class EditThreadCommandValidator : AbstractValidator<EditThreadCommand>
    {
        public EditThreadCommandValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.ForumId).NotNull().WithMessage("ForumId cannot be empty.");
            RuleFor(x => x.ThreadId).NotNull().WithMessage("ThreadId cannot be empty.");
            RuleFor(x => x.Subject).NotNull().NotEmpty().WithMessage("Subject cannot be empty.");
        }
    }
}