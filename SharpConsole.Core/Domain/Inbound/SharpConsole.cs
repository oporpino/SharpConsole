using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole;

public abstract class Console : ISharpConsole
{
  public virtual object GetContext()
  {
    return this;
  }
}
