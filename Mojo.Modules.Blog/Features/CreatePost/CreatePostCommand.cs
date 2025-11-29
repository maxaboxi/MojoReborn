using FluentValidation;

namespace Mojo.Modules.Blog.Features.CreatePost;

public class CreatePostCommand(int pageId, string author, string title, string subtitle, string content, List<CreatePostCategoryDto> categories)
{
    public int PageId { get; set; } = pageId;
    public string Author { get; set; } = author;
    public string Title { get; set; } = title;
    public string SubTitle { get; set; } = subtitle;
    public string Content { get; set; } = content;
    
    public List<CreatePostCategoryDto> Categories { get; set; } = categories;

    public class CreatePostValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.PageId).NotNull();
            RuleFor(x => x.Author).NotNull().MaximumLength(100);
            RuleFor(x => x.Title).NotNull().MaximumLength(255);
            RuleFor(x => x.SubTitle).NotNull().MaximumLength(500);
            RuleFor(x => x.Content).NotNull();
            RuleFor(x => x.Categories).NotNull();
        }
    }
}