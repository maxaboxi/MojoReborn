using FluentValidation;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Forum.Features.Posts.CreatePost;

public record CreateForumPostCommand(int PageId, int ForumId, int ThreadId, string Post, Guid? ReplyToPost) : IFeatureRequest
{
    public string Name => FeatureNames.Forum;
    public bool RequiresEditPermission => true;
    public bool UserCanEdit => false;
    public string? UserIpAddress { get; set; }
    public class CreateForumPostCommandValidator : AbstractValidator<CreateForumPostCommand>
    {
        public CreateForumPostCommandValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.ForumId).NotNull().WithMessage("ForumId cannot be empty.");
            RuleFor(x => x.ThreadId).NotNull().WithMessage("ThreadId cannot be empty.");
            RuleFor(x => x.Post).NotNull().NotEmpty().WithMessage("Post cannot be empty.");
        }
    }
}