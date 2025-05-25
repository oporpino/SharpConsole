using SharpConsoleCore.Domain.Inbound;

namespace SharpConsoleCore.Application;

public static class ConsoleExtensions
{
  public static void Start(this ISharpConsole console)
  {
    Console.Start(console.GetContext());
  }
}
