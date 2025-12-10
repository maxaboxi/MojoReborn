namespace Mojo.Modules.Core.Features.SiteStructure.GetMenu;

public class PageDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ModuleTitle { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ViewRoles { get; set; } = string.Empty;
    public int Order { get; set; }
    
    public List<PageDto> Children { get; set; } = [];
}