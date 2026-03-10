using System.Text.Json.Serialization;
using FluentValidation;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Blog.Features.Comments.CreateComment;

public record CreateBlogCommentCommand(int PageId, Guid BlogPostId, string Author, string Title, string Content) : IFeatureRequest
{
    public string Name => FeatureNames.Blog;
    public bool RequiresEditPermission => true;
    public bool UserCanEdit => false;
    [JsonIgnore]
    public string? UserIpAddress { get; set; }
    public class CreateBlogCommentValidator : AbstractValidator<CreateBlogCommentCommand>
    {
        public CreateBlogCommentValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be empty.");
            RuleFor(x => x.BlogPostId).NotNull().WithMessage("BlogPostId cannot be empty.");
            RuleFor(x => x.Author).NotNull().WithMessage("Author cannot be empty.");
            RuleFor(x => x.Title).NotNull().WithMessage("Title cannot be empty.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");
            RuleFor(x => x.Content).NotNull().WithMessage("Content cannot be empty.");
        }
    }
}