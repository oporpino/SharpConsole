using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole.Domain.Entities;

public abstract class SharpConsoleBase : ISharpConsole
{
  public virtual object GetContext()
  {
    return this;
  }
}
