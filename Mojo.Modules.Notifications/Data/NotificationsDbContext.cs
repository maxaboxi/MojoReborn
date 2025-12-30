using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Notifications.Domain.Entities;

namespace Mojo.Modules.Notifications.Data;

public class NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : DbContext(options)
{
    public DbSet<UserNotification>  UserNotifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsDbContext).Assembly);
    }
}