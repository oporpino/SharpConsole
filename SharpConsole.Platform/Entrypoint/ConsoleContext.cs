

namespace SharpConsole.Platform.Entrypoint;

public abstract class ConsoleContext : IConsoleContext
{
  public virtual object GetContext()
  {
    return this;
  }
}
