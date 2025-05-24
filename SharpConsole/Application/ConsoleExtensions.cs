using SharpConsole.Domain.Inbound;

namespace SharpConsole.Application;

public static class ConsoleExtensions
{
  private static bool _isInitialized;

  public static async Task RunConsoleAsync(this IContext context)
  {
    if (!_isInitialized)
    {
      ConsoleModule.Initialize();
      _isInitialized = true;
    }

    await Console.Start(context);
  }
}
