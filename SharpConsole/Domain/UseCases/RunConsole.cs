using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Domain.UseCases;

public class RunConsole
{
  private readonly IConsole _console;
  private readonly IConsoleDisplay _consoleUI;
  private bool _isRunning;

  public RunConsole(IConsole console, IConsoleDisplay consoleUI)
  {
    _console = console;
    _consoleUI = consoleUI;
    _isRunning = true;
  }

  public void Stop()
  {
    _isRunning = false;
  }

  public async Task Execute()
  {
    await _console.ShowWelcome();

    while (_isRunning)
    {
      var input = _consoleUI.ReadInput();
      if (string.IsNullOrWhiteSpace(input)) continue;
      if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
      {
        _isRunning = false;
        continue;
      }

      await _console.ExecuteCommand(input);
    }
  }
}
