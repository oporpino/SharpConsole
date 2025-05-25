using SharpConsoleCore.Application;
using SharpConsoleCore.Domain.Entities;

namespace SharpConsoleCore.Example;

public class Program
{
  public static void Main(string[] args)
  {
    var console = new CustomConsole();
    console.Start();
  }
}

public class CustomConsole : SharpConsole
{
  public string Name { get; set; } = "Test";
  public int Age { get; set; } = 25;
  public string[] Tags { get; set; } = new[] { "tag1", "tag2" };
}
