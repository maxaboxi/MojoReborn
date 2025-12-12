using Microsoft.EntityFrameworkCore;
using Mojo.Modules.SiteStructure.Domain.Entities;

namespace Mojo.Modules.SiteStructure.Data;

public class SiteStructureDbContext(DbContextOptions<SiteStructureDbContext> options) : DbContext(options)
{
    public DbSet<Page> Pages { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<PageModule> PageModules { get; set; }
    public DbSet<ModuleDefinition> ModuleDefinitions { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<SiteHost> SiteHosts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteStructureDbContext).Assembly);
    }
}