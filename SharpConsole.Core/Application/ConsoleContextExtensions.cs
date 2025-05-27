using SharpConsole.Core.Application.Internal;
using Console = SharpConsole.Core.Application.Internal.Console;

namespace SharpConsole.Core.Application;

public static class ConsoleContextExtensions
{
  public static void StartConsole(this IConsoleContext context)
  {
    Console.Start(context.GetContext());
  }
}
