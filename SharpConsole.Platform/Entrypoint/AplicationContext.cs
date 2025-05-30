using SharpConsole.Core.Inbound;

namespace SharpConsole.Platform.Entrypoint;

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
