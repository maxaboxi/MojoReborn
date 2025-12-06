namespace Mojo.Modules.Core.Features.SiteStructure.Entities;

public class PageModule
{
    public int Id { get; set; }
    public int PageId { get; set; }
    public virtual Page Page { get; set; }
    public int ModuleId { get; set; }
    public virtual Module Module { get; set; }
    public Guid PageGuid { get; set; }
    public Guid ModuleGuid { get; set; }
    public string PaneName { get; set; }
    public int ModuleOrder { get; set; }
}