using SharpConsole.Core.Outbound;
using SharpConsole.Core.Application.UseCases;

namespace SharpConsole.Core;

public class CoreFacade
{
  private readonly IConsoleExecutor _executor;
  private readonly IConsoleDisplay _display;

  public CoreFacade(IConsoleExecutor executor, IConsoleDisplay display)
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
