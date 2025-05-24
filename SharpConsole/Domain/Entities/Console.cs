using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Domain.Entities;

public class Console : IConsole
{
  private readonly IScriptEngine _scriptEngine;
  private readonly IConsoleDisplay _consoleUI;
  private readonly ICommandHistory _commandHistory;

  public Console(IScriptEngine scriptEngine, IConsoleDisplay consoleUI, ICommandHistory commandHistory)
  {
    _scriptEngine = scriptEngine;
    _consoleUI = consoleUI;
    _commandHistory = commandHistory;
  }

  public async Task ExecuteCommand(string command)
  {
    try
    {
      _commandHistory.AddCommand(command);
      var result = await _scriptEngine.Execute(command);
      await ShowResult(result);
    }
    catch (Exception ex)
    {
      await ShowError(ex.Message);
    }
  }

  public Task ShowResult(object? result)
  {
    _consoleUI.ShowResult(result);
    return Task.CompletedTask;
  }

  public Task ShowError(string message)
  {
    _consoleUI.ShowError(message);
    return Task.CompletedTask;
  }

  public Task ShowWelcome()
  {
    _consoleUI.ShowWelcome();
    return Task.CompletedTask;
  }
}
