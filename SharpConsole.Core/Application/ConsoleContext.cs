using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole.Core.Application;

public static class ConsoleContext
{
  private static dynamic? _context;

  public static void Set(dynamic context)
  {
    _context = context;
  }

  public static dynamic Get()
  {
    return _context ?? throw new InvalidOperationException("Context not set");
  }
}
