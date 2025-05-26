

namespace SharpConsole.Core.Application;

public abstract class SharpConsoleBase : ISharpConsole
{
  public virtual object GetContext()
  {
    return this;
  }
}
