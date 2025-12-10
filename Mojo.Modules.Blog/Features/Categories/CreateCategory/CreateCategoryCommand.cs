using FluentValidation;

namespace Mojo.Modules.Blog.Features.Categories.CreateCategory;

public record CreateCategoryCommand(int PageId, string CategoryName)
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category name cannot be empty");
            RuleFor(x => x.PageId).NotEmpty().WithMessage("PageId cannot be empty");
        }
    }
}