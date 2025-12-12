using Microsoft.EntityFrameworkCore;

namespace Mojo.Modules.Core.Data;

public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{

}