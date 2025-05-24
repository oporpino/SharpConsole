using ConsoleEntity = SharpConsole.Domain.Entities.Console;
using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Domain.UseCases;

public class CreateConsole
{
  private readonly IScriptEngine _scriptEngine;
  private readonly IConsoleDisplay _consoleUI;
  private readonly ICommandHistory _commandHistory;

  public CreateConsole(IScriptEngine scriptEngine, IConsoleDisplay consoleUI, ICommandHistory commandHistory)
  {
    _scriptEngine = scriptEngine;
    _consoleUI = consoleUI;
    _commandHistory = commandHistory;
  }

  public IConsole Execute()
  {
    return new ConsoleEntity(_scriptEngine, _consoleUI, _commandHistory);
  }
}
