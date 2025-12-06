using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Domain;
using Mojo.Modules.Blog.Domain.Entities;

namespace Mojo.Modules.Blog.Data;

public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
{
    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<BlogCategory> Categories { get; set; }

    public virtual DbSet<BlogComment> BlogComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
        
        modelBuilder.Entity<BlogComment>()
            .HasQueryFilter(x => this.Set<BlogPost>().Any(b => b.BlogPostId == x.ContentGuid));
    }
    
}