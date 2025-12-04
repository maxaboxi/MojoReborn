namespace Mojo.Modules.Core.Data;

public class Module
{
    public int Id { get; set; }
    public int SiteId { get; set; }
    public Guid SiteGuid { get; set; }
    public Guid FeatureGuid { get; set; }
    public int ModuleDefinitionId { get; set; }
    public Guid ModuleGuid { get; set; }
    public string Title { get; set; }
    public string AuthorizedEditRoles { get; set; } // Legacy column: "Admins;Editors;"
    public DateTime CreatedAt { get; set; }
    public int? CreatedByUserId { get; set; }
    
    public ICollection<PageModule> PageModules { get; set; } = new List<PageModule>();
    public ModuleDefinition ModuleDefinition { get; set; }
}