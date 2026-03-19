using FluentValidation;
using Mojo.Shared.Domain;
using Mojo.Shared.Interfaces.Security;

namespace Mojo.Modules.Blog.Features.Categories.EditCategory;

public record EditCategoryCommand(int PageId, int CategoryId, string CategoryName) : IFeatureRequest
{
    public string Name => FeatureNames.Blog;
    public bool RequiresEditPermission => true;
    public bool UserCanEdit => false;

    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(x => x.PageId).GreaterThan(0).WithMessage("PageId must be greater than zero.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than zero.");
            RuleFor(x => x.CategoryName).NotNull().WithMessage("CategoryName cannot be empty");
        }
    }
}