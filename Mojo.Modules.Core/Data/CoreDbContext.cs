using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Core.Features.Identity.Entities;
using Mojo.Modules.Core.Features.SiteStructure.Entities;

namespace Mojo.Modules.Core.Data;

public class CoreDbContext(DbContextOptions<CoreDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<Page> Pages { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<PageModule> PageModules { get; set; }
    public DbSet<ModuleDefinition> ModuleDefinitions { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<SiteHost> SiteHosts { get; set; }
    public DbSet<LegacyUser> LegacyUsers { get; set; }
    public DbSet<LegacyRole> LegacyRoles { get; set; }
    
    public DbSet<UserSiteProfile> UserSiteProfiles { get; set; }
    public DbSet<SiteRole> SiteRoles { get; set; }
    public DbSet<UserSiteRoleAssignment> UserSiteRoleAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreDbContext).Assembly);
    }
}