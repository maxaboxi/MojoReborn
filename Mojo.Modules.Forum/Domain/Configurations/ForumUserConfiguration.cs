using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojo.Modules.Forum.Domain.Entities;

namespace Mojo.Modules.Forum.Domain.Configurations;

public class ForumUserConfiguration : IEntityTypeConfiguration<ForumUser>
{
    public void Configure(EntityTypeBuilder<ForumUser> builder)
    {
        builder.ToTable("mp_Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("UserID");
        builder.Property(x => x.UserGuid).HasColumnName("UserGuid");
        builder.Property(x => x.DisplayName).HasColumnName("Name");
        
        builder.Metadata.SetIsTableExcludedFromMigrations(true);
    }
}