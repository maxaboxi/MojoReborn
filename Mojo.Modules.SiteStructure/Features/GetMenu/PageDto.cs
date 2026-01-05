namespace Mojo.Modules.SiteStructure.Features.GetMenu;

public class PageDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public Guid ModuleGuid { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FeatureName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ViewRoles { get; set; } = string.Empty;
    public int Order { get; set; }
    
    public List<PageDto> Children { get; set; } = [];
}