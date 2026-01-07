using FluentValidation;
using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Categories.CreateCategory;

public record CreateCategoryCommand(int PageId, string CategoryName)
{
    public string Name => FeatureNames.Blog;

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category name cannot be empty");
            RuleFor(x => x.PageId).NotEmpty().WithMessage("PageId cannot be empty");
        }
    }
}