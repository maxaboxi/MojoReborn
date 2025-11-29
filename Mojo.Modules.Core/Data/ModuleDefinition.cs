namespace Mojo.Modules.Core.Data;

public class ModuleDefinition
{
    public int Id { get; set; }
    public string FeatureName { get; set; } = null!;
    public string ControlSrc { get; set; } = null!;

    public int SortOrder { get; set; }

    public bool IsAdmin { get; set; }

    public Guid ModuleDefinitionGuid { get; set; }

    public string? ResourceFile { get; set; }


    public bool? IsSearchable { get; set; }

    public string? SearchListName { get; set; }
    

    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
}