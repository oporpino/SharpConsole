using SharpConsole.Domain.Inbound;

namespace SharpConsole.Application;

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
