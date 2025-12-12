namespace Mojo.Modules.SiteStructure.Domain.Entities;

public class Site
{
    public int SiteId { get; set; }

    public Guid SiteGuid { get; set; }

    public string? SiteAlias { get; set; }

    public string SiteName { get; set; } = null!;

    public string? Logo { get; set; }

    public string? Icon { get; set; }

    public bool ReallyDeleteUsers { get; set; }
    
    public ICollection<Page> Pages { get; set; } = [];
    public ICollection<SiteHost> SiteHosts { get; set; } = [];

}