using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole.Core.Domain.Entities;

public abstract class SharpConsoleBase : ISharpConsole
{
  public virtual object GetContext()
  {
    return this;
  }
}
