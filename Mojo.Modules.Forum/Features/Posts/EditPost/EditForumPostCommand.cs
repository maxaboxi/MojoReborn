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
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.ForumId).NotNull().WithMessage("ForumId cannot be empty.");
            RuleFor(x => x.ThreadId).NotNull().WithMessage("ThreadId cannot be empty.");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("Content cannot be empty.");
        }
    }
}