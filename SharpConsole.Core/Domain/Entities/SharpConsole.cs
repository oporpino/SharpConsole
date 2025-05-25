using SharpConsoleCore.Domain.Inbound;

namespace SharpConsoleCore.Domain.Entities;

public abstract class SharpConsole : ISharpConsole
{
  public virtual object GetContext()
  {
    return this;
  }
}
