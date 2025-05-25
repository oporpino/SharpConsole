using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Domain.UseCases;

public class RunConsole
{
  private readonly IConsoleExecutor _executor;
  private readonly IConsoleDisplay _consoleUI;

  public RunConsole(IConsoleExecutor executor, IConsoleDisplay consoleUI)
  {
    _executor = executor;
    _consoleUI = consoleUI;
  }

  public void Execute()
  {
    _executor.Execute();
  }
}
