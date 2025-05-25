using SharpConsoleCore.Domain.Entities;
using SharpConsoleCore.Domain.Outbound;
using SharpConsoleCore.Domain.Inbound;

namespace SharpConsoleCore.Domain.UseCases;

public class CreateConsoleUsecase : UseCase<IConsoleExecutor>
{
  private readonly IScriptEngine _scriptEngine;
  private readonly IConsoleDisplay _console;
  private readonly ICommandHistory _commandHistory;

  public CreateConsoleUsecase(
      IScriptEngine scriptEngine,
      IConsoleDisplay console,
      ICommandHistory commandHistory)
  {
    _scriptEngine = scriptEngine;
    _console = console;
    _commandHistory = commandHistory;
  }

  public override IConsoleExecutor Execute()
  {
    return new ConsoleExecutor(_scriptEngine, _console, _commandHistory);
  }
}
