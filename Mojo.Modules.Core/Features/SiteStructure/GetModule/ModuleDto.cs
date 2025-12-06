namespace Mojo.Modules.Core.Features.SiteStructure.GetModule;

public class ModuleDto
{
    public int Id { get; set; }
    public Guid ModuleGuid { get; set; }
    public int SiteId { get; set; }
    public Guid SiteGuid { get; set; }
    public Guid FeatureGuid { get; set; }
}