using System;
using System.Threading.Tasks;
using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Domain.Entities;

public class ConsoleExecutor : IConsoleExecutor
{
  private readonly IScriptEngine _scriptEngine;
  private readonly IConsoleDisplay _console;
  private readonly ICommandHistory _commandHistory;
  private bool _isRunning;

  public ConsoleExecutor(IScriptEngine scriptEngine, IConsoleDisplay consoleDisplay, ICommandHistory commandHistory)
  {
    _scriptEngine = scriptEngine;
    _console = consoleDisplay;
    _commandHistory = commandHistory;
    _isRunning = true;
  }

  public void Stop()
  {
    _isRunning = false;
  }

  public void Execute()
  {
    Start().Wait();
  }

  public async Task Start()
  {
    await ShowWelcome();

    while (_isRunning)
    {
      var input = _console.ReadInput();
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
    _console.ShowResult(result);
    return Task.CompletedTask;
  }

  public Task ShowError(string message)
  {
    _console.ShowError(message);
    return Task.CompletedTask;
  }

  public Task ShowWelcome()
  {
    _console.ShowWelcome();
    return Task.CompletedTask;
  }
}
