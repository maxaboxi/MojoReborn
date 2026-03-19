using FluentValidation;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Forum.Features.Posts.EditPost;

public record EditForumPostCommand(int PageId, int ForumId, int ThreadId, int PostId, string Content) : IFeatureRequest
{
    public string Name => FeatureNames.Forum;
    public bool RequiresEditPermission => true;
    public bool UserCanEdit => true;

    public class EditForumPostCommandValidator : AbstractValidator<EditForumPostCommand>
    {
        public EditForumPostCommandValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.ForumId).GreaterThan(0).WithMessage("ForumId must be greater than zero.");
            RuleFor(x => x.ThreadId).GreaterThan(0).WithMessage("ThreadId must be greater than zero.");
            RuleFor(x => x.PostId).GreaterThan(0).WithMessage("PostId must be greater than zero.");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("Content cannot be empty.");
        }
    }
}