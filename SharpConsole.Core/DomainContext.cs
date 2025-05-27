using SharpConsole.Core.Outbound;
using SharpConsole.Core.UseCases;

namespace SharpConsole.Core;

public class DomainContext
{
  private readonly IConsoleExecutor _executor;
  private readonly IConsoleDisplay _display;

  public DomainContext(IConsoleExecutor executor, IConsoleDisplay display)
  {
    _executor = executor;
    _display = display;
  }

  public void RunConsole()
  {
    var runConsole = new RunConsole(_executor, _display);
    runConsole.Execute();
  }




}
