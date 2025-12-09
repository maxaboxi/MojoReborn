namespace Mojo.Shared.Dtos.SiteStructure;

public class ModuleDto
{
    public int Id { get; set; }
    public Guid ModuleGuid { get; set; }
    public Guid FeatureGuid { get; set; }
    
    // Site info does not technically belong here, but it is needed (for creating a blog comment for example)
    // so it doesn't make sense to not have it here. Though the site info is most likely already cached
    // in SiteResolver and wouldn't warrant a second trip to the database.
    public int SiteId { get; set; }
    public Guid SiteGuid { get; set; }
}