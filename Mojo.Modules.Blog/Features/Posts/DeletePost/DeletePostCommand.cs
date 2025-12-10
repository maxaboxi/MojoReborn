using FluentValidation;

namespace Mojo.Modules.Blog.Features.Posts.DeletePost;

public record DeletePostCommand(int PageId, Guid Id)
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.Id).NotNull().WithMessage("Id cannot be empty.");
        }
    }
}