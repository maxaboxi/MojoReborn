using System.Text.Json.Serialization;
using FluentValidation;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Forum.Features.Posts.CreatePost;

public record CreateForumPostCommand(int PageId, int ForumId, int ThreadId, string Post, Guid? ReplyToPost) : IFeatureRequest
{
    public string Name => FeatureNames.Forum;
    public bool RequiresEditPermission => true;
    public bool UserCanEdit => false;
    [JsonIgnore]
    public string? UserIpAddress { get; set; }
    public class CreateForumPostCommandValidator : AbstractValidator<CreateForumPostCommand>
    {
        public CreateForumPostCommandValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.ForumId).GreaterThan(0).WithMessage("ForumId must be greater than zero.");
            RuleFor(x => x.ThreadId).GreaterThan(0).WithMessage("ThreadId must be greater than zero.");
            RuleFor(x => x.Post).NotNull().NotEmpty().WithMessage("Post cannot be empty.");
        }
    }
}