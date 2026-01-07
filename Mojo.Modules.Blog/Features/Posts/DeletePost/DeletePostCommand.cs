using FluentValidation;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Posts.DeletePost;

public record DeletePostCommand(int PageId, Guid Id)
{
    public string Name => FeatureNames.Blog;

    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.Id).NotNull().WithMessage("Id cannot be empty.");
        }
    }
}