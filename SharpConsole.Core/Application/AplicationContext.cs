using SharpConsole.Domain.Inbound;

namespace SharpConsole.Core.Application;

public static class AplicationContext
{
  private static dynamic? _context;

  internal static void Set(dynamic context)
  {
    _context = context;
  }

  public static dynamic Get()
  {
    return _context ?? throw new InvalidOperationException("Context not set");
  }
}
