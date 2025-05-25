using SharpConsoleCore.Domain.Inbound;

namespace SharpConsoleCore.Domain.Inbound;

public abstract class SharpConsole : ISharpConsole
{
  public virtual object GetContext()
  {
    return this;
  }
}
