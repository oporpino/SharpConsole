using SharpConsole.Domain.Inbound;

namespace SharpConsole.Application;

public static class ConsoleContext
{
  private static IContext? _context;

  public static void SetContext(IContext context)
  {
    _context = context;
  }

  public static IContext GetContext()
  {
    return _context ?? throw new InvalidOperationException("Context not set");
  }
}
