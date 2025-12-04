namespace Mojo.Modules.Core.Data;

public class Page
{
    public int PageId { get; set; }
    public int SiteId { get; set; }
    public Guid SiteGuid { get; set; }
    public int? ParentId { get; set; }
    public Guid? ParentGuid { get; set; }
    public string PageName { get; set; }
    public string Url { get; set; }
    public int PageOrder { get; set; }
    public string AuthorizedRoles { get; set; } // Legacy column: "Admins;Editors;"
    public bool IncludeInMenu { get; set; }
    
    public ICollection<PageModule> PageModules { get; set; } = new List<PageModule>();
}