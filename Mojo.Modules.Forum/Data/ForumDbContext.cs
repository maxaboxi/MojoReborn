using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Forum.Domain.Entities;

namespace Mojo.Modules.Forum.Data;

public class ForumDbContext(DbContextOptions<ForumDbContext> options) : DbContext(options)
{
    public DbSet<ForumEntity> Forums { get; set; }
    public DbSet<ForumThread> ForumThreads { get; set; }
    public DbSet<ForumPost> ForumPosts { get; set; }
}