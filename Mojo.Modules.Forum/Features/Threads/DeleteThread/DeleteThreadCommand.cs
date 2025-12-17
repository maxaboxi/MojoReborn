using FluentValidation;

namespace Mojo.Modules.Forum.Features.Threads.DeleteThread;

public record DeleteThreadCommand(int PageId, int ForumId, int ThreadId)
{
    public class DeleteThreadCommandValidator : AbstractValidator<DeleteThreadCommand>
    {
        public DeleteThreadCommandValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.ForumId).NotNull().WithMessage("ForumId cannot be empty.");
            RuleFor(x => x.ThreadId).NotNull().WithMessage("ThreadId cannot be empty.");
        }
    }
}