using SharpConsole.Application;
using SharpConsole.Domain.Inbound;

namespace SharpConsole.Example;

public class Program
{
  public static async Task Main(string[] args)
  {
    var console = new CustomContext();

    console.Start();
  }
}

public class CustomConsole : ISharpConsole
{
  public object GetContext()
  {
    return new Context { Name = "Test" };
  }
}

public class Context
{
  public string Name { get; set; } = string.Empty;
}
