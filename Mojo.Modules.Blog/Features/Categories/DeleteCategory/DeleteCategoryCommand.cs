using FluentValidation;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Blog.Features.Categories.DeleteCategory;

public record DeleteCategoryCommand(int PageId, int CategoryId) : IFeatureRequest
{
    public string Name => FeatureNames.Blog;
    public bool RequiresEditPermission => true;
    public bool UserCanEdit => false;

    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.PageId).NotNull().WithMessage("PageId cannot be null");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("CategoryId cannot be null");
        }
    }
}