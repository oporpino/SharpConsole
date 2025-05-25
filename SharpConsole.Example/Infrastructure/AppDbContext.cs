using Microsoft.EntityFrameworkCore;

namespace SharpConsole.Example.Infrastructure;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public DbSet<User> Users { get; set; }
}
