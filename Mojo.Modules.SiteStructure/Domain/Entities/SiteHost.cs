namespace Mojo.Modules.SiteStructure.Domain.Entities;

public class SiteHost
{
    public int HostId { get; set; }

    public int SiteId { get; set; }

    public string HostName { get; set; } = null!;

    public Guid SiteGuid { get; set; }
    public virtual Site Site { get; set; }
}