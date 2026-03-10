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
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than zero.");
        }
    }
}