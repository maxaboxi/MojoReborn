using FluentValidation;

namespace Mojo.Modules.Forum.Features.Threads.CreateThread;

public record CreateThreadCommand(int PageId, int ForumId, string Subject)
{
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