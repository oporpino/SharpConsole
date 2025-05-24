using SharpConsole.Domain.Entities;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.Inbound;

namespace SharpConsole.Domain.UseCases;

public class CreateConsoleUsecase : UseCase<IConsole>
{
  private readonly IScriptEngine _scriptEngine;
  private readonly IConsoleDisplay _consoleUI;
  private readonly ICommandHistory _commandHistory;

  public CreateConsoleUsecase(
      IScriptEngine scriptEngine,
      IConsoleDisplay consoleUI,
      ICommandHistory commandHistory)
  {
    _scriptEngine = scriptEngine;
    _consoleUI = consoleUI;
    _commandHistory = commandHistory;
  }

  public override IConsole Execute()
  {
    return new ConsoleEntity(_scriptEngine, _consoleUI, _commandHistory);
  }
}
