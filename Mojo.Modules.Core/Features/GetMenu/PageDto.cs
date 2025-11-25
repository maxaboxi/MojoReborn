namespace Mojo.Modules.Core.Features.GetMenu;

public class PageDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ViewRoles { get; set; } = string.Empty; // For security filtering later
    public int Order { get; set; }
    
    // The recursive part
    public List<PageDto> Children { get; set; } = [];
}