using SharpConsole.Core.Application.Internal;
using Console = SharpConsole.Core.Application.Internal.Console;

namespace SharpConsole.Core.Application;

public static class ConsoleExtensions
{
  public static void Start(this ISharpConsole console)
  {
    Console.Start(console.GetContext());
  }
}
