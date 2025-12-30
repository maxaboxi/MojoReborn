using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Forum.Data;
using Mojo.Modules.Identity.Data;
using Mojo.Modules.Identity.Data.Seeding;
using Mojo.Modules.Notifications.Data;
using Mojo.Modules.SiteStructure.Data;

namespace Mojo.Web.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
    
        logger.LogInformation("Starting database migrations.");
    
        var identityDb = services.GetRequiredService<IdentityDbContext>();
        await identityDb.Database.MigrateAsync();
        logger.LogInformation("IdentityDbContext migrations applied successfully.");
        
        await LegacyRoleSeeder.SeedAsync(identityDb, logger);
        
        var forumDb = services.GetRequiredService<ForumDbContext>();
        await forumDb.Database.MigrateAsync();
        logger.LogInformation("ForumDbContext migrations applied successfully.");
        
        var blogDb = services.GetRequiredService<BlogDbContext>();
        await blogDb.Database.MigrateAsync();
        logger.LogInformation("BlogDbContext migrations applied successfully.");
        
        var siteDb = services.GetRequiredService<SiteStructureDbContext>();
        await siteDb.Database.MigrateAsync();
        logger.LogInformation("SiteStructureDbContext migrations applied successfully.");
        
        var notificationsDb = services.GetRequiredService<NotificationsDbContext>();
        await notificationsDb.Database.MigrateAsync();
        logger.LogInformation("NotificationsDbContext migrations applied successfully.");
        
        logger.LogInformation("All database migrations applied successfully.");
    }
}