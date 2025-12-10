using FluentValidation;

namespace Mojo.Modules.Blog.Features.Categories.DeleteCategory;

public record DeleteCategoryCommand(int PageId, int CategoryId)
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be null");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("CategoryId cannot be null");
        }
    }
}