using SharpConsole.Domain.Inbound;

namespace SharpConsole.Application;

public static class ConsoleExtensions
{
  public static async Task RunConsoleAsync(this IContext context)
  {
    await Console.Start(context);
  }
}
