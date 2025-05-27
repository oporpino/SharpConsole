using SharpConsole.Platform.Entrypoint.Internal;
using Console = SharpConsole.Platform.Entrypoint.Internal.Console;

namespace SharpConsole.Platform.Entrypoint;

public static class ConsoleContextExtensions
{
  public static void StartConsole(this IConsoleContext context)
  {
    Console.Start(context.GetContext());
  }
}
