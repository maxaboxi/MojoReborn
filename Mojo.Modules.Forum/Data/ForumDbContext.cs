using Microsoft.EntityFrameworkCore;

namespace Mojo.Modules.Forum.Data;

public class ForumDbContext(DbContextOptions<ForumDbContext> options) : DbContext(options)
{
    
}