using SharpConsole.Platform.Application.Internal;
using Console = SharpConsole.Platform.Application.Internal.Console;

namespace SharpConsole.Platform.Application;

public static class ConsoleContextExtensions
{
  public static void StartConsole(this IConsoleContext context)
  {
    Console.Start(context.GetContext());
  }
}
