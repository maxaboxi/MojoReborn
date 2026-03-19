using FluentValidation;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Blog.Features.Posts.DeletePost;

public record DeletePostCommand(int PageId, Guid Id) : IFeatureRequest
{
    public string Name => FeatureNames.Blog;
    public bool RequiresEditPermission => true;
    public bool UserCanEdit => true;

    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.Id).NotNull().WithMessage("Id cannot be empty.");
        }
    }
}