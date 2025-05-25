using SharpConsoleCore.Application;
using SharpConsoleCore.Domain.Entities;

namespace SharpConsoleCore.Example;

public class Program
{
  public static void Main(string[] args)
  {
    var console = new CustomConsole()
    {
      Name = "Test",
      Age = 25,
      Tags = new[] { "tag1", "tag2" }
    };

    console.Start();
  }
}

public class CustomConsole : SharpConsole
{
  public string Name { get; set; }
  public int Age { get; set; }
  public string[] Tags { get; set; }
}
