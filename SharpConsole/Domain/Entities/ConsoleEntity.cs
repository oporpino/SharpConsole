using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Domain.Entities;

public class ConsoleEntity : IConsole
{
  private readonly IScriptEngine _scriptEngine;
  private readonly IConsoleDisplay _consoleUI;
  private readonly ICommandHistory _commandHistory;
  private bool _isRunning;

  public ConsoleEntity(IScriptEngine scriptEngine, IConsoleDisplay consoleUI, ICommandHistory commandHistory)
  {
    _scriptEngine = scriptEngine;
    _consoleUI = consoleUI;
    _commandHistory = commandHistory;
    _isRunning = true;
  }

  public void Stop()
  {
    _isRunning = false;
  }

  public async Task Start()
  {
    await ShowWelcome();

    while (_isRunning)
    {
      var input = _consoleUI.ReadInput();
      if (string.IsNullOrWhiteSpace(input)) continue;
      if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
      {
        _isRunning = false;
        continue;
      }

      await ExecuteCommand(input);
    }
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
