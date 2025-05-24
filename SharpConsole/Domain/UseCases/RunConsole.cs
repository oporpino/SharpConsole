using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Domain.UseCases;

public class RunConsole
{
  private readonly IConsole _console;
  private readonly IConsoleUI _consoleUI;

  public RunConsole(IConsole console, IConsoleUI consoleUI)
  {
    _console = console;
    _consoleUI = consoleUI;
  }

  public async Task Execute()
  {
    await _console.ShowWelcome();
    var isRunning = true;

    while (isRunning)
    {
      var input = _consoleUI.ReadInput();
      if (string.IsNullOrWhiteSpace(input)) continue;
      if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
      {
        isRunning = false;
        continue;
      }

      await _console.ExecuteCommand(input);
    }
  }
}
