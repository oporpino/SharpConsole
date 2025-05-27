

namespace SharpConsole.Platform.Application;

public abstract class ConsoleContext : IConsoleContext
{
  public virtual object GetContext()
  {
    return this;
  }
}
