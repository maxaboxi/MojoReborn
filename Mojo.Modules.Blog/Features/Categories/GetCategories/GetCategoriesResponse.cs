using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Categories.GetCategories;

public class GetCategoriesResponse : BaseResponse
{
    public List<CategoryDto> Categories { get; set; } = [];
}