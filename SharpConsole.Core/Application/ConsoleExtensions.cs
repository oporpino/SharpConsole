using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole.Core.Application;

public static class ConsoleExtensions
{
  public static void Start(this ISharpConsole console)
  {
    Console.Start(console.GetContext());
  }
}
