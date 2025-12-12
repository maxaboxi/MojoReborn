using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Identity.Domain.Entities;

namespace Mojo.Modules.Identity.Domain.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("AspNetUsers");
            
        builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(100).IsRequired();
        builder.Property(u => u.DisplayName).HasMaxLength(50);
        builder.Property(u => u.AvatarUrl).HasMaxLength(255);
        builder.Property(u => u.Bio).HasMaxLength(500);
        builder.Property(u => u.Signature).HasMaxLength(255);
        builder.Property(u => u.TimeZoneId).HasMaxLength(100);
    }
}