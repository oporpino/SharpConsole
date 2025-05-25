using Microsoft.EntityFrameworkCore;

namespace SharpConsole.Example.Infrastructure;

public class AppDbContext : DbContext
{
  public DbSet<User> Users { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
}
