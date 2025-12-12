using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Identity.Data;
using Mojo.Modules.Identity.Data.Seeding;

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
        logger.LogInformation("CoreDbContext migrations applied successfully.");
        
        await LegacyRoleSeeder.SeedAsync(identityDb, logger);
        
        logger.LogInformation("All database migrations applied successfully.");
    }
}