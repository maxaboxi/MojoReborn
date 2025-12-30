using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Notifications.Domain.Entities;

namespace Mojo.Modules.Notifications.Domain.Configurations;

public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
{
    public void Configure(EntityTypeBuilder<UserNotification> builder)
    {
        builder.ToTable("UserNotifications");

        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ModuleGuid);
        
        builder.Property(x => x.FeatureName).HasMaxLength(50);
        builder.Property(x => x.Message).HasMaxLength(300);
        builder.Property(x => x.Url).HasMaxLength(300);
    }
}