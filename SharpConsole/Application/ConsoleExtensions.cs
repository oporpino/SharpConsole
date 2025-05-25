using SharpConsole.Domain.Inbound;

namespace SharpConsole.Application;

public static class ConsoleExtensions
{
  public static void Start(this ISharpConsole console)
  {
    Console.Start(console.GetContext());
  }
}
