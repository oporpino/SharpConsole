using SharpConsole.Core.Inbound;
using SharpConsole.Core.Outbound;

namespace SharpConsole.Core.UseCases;

public class RunConsole : UseCase<RunConsole>
{
  private readonly IConsoleExecutor _executor;
  private readonly IConsoleDisplay _consoleDisplay;

  public RunConsole(IConsoleExecutor executor, IConsoleDisplay consoleDisplay)
  {
    _executor = executor;
    _consoleDisplay = consoleDisplay;
  }

  public override void Execute()
  {
    _executor.Execute();
  }
}
