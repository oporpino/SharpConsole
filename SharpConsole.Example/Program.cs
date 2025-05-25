using Microsoft.EntityFrameworkCore;
using SharpConsoleCore.Application;
using SharpConsole.Example.Infrastructure;
using Console = SharpConsoleCore.Domain.Entities.SharpConsole;

namespace SharpConsole.Example;

public class Program
{
  public static void Main(string[] args)
  {
    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName: "ExampleDb")
      .Options;

    using var context = new AppDbContext(options);

    context.Users.Add(new User { Name = "Guga", Age = 38 });
    context.Users.Add(new User { Name = "Bia", Age = 13 });
    context.SaveChanges();

    var console = new CustomConsole()
    {
      db = context,
      Name = "Guga",
      Age = 38,
      Tags = new[] { "tag1", "tag2" }
    };

    console.Start();
  }
}

public class CustomConsole : Console
{
  public string Name { get; set; }

  public int Age { get; set; }

  public string[] Tags { get; set; }

  public AppDbContext db;
}
