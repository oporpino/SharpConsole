using SharpConsole.Domain.Inbound;
using SharpConsole;

namespace SharpConsole;

public static class ConsoleExtensions
{
  public static async Task RunConsoleAsync(this IContext context)
  {
    await SharpConsole.Application.Console.Start(context);
  }
}
