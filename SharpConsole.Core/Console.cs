using SharpConsoleCore.Domain.Inbound;

namespace SharpConsoleCore;

public abstract class SharpConsole : ISharpConsole
{
  public virtual object GetContext()
  {
    return this;
  }
}
