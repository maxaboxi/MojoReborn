using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Identity.Domain.Entities;

namespace Mojo.Modules.Identity.Data;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<LegacyUser> LegacyUsers { get; set; }
    public DbSet<LegacyRole> LegacyRoles { get; set; }
    
    public DbSet<UserSiteProfile> UserSiteProfiles { get; set; }
    public DbSet<SiteRole> SiteRoles { get; set; }
    public DbSet<UserSiteRoleAssignment> UserSiteRoleAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}