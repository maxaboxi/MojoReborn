using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Core.Data;

namespace Mojo.Web.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
    
        logger.LogInformation("Starting database migrations.");
    
        var coreDb = services.GetRequiredService<CoreDbContext>();
        await coreDb.Database.MigrateAsync();
        logger.LogInformation("CoreDbContext migrations applied successfully.");
        
        logger.LogInformation("All database migrations applied successfully.");
    }
}