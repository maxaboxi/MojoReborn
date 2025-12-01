namespace Mojo.Modules.Blog.Features.GetCategories;

public class GetCategoriesResponse
{
    public int Id { get; set; }

    public int ModuleId { get; set; }

    public string CategoryName { get; set; } = null!;
}