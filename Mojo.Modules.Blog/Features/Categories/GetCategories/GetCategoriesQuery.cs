using Mojo.Shared.Domain;

namespace Mojo.Modules.Blog.Features.Categories.GetCategories;

public record GetCategoriesQuery(int PageId)
{
    public string Name => FeatureNames.Blog;
}