using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole.Core.Domain.Inbound;

public abstract class SharpConsole : ISharpConsole
{
  public virtual object GetContext()
  {
    return this;
  }
}
