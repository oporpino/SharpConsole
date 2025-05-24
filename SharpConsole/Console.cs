using SharpConsole.Core;
using SharpConsole.Scripting;
using SharpConsole.UI;

namespace SharpConsole;

public class Console
{
  private readonly IContext _context;
  private readonly IScriptEngine _scriptEngine;
  private readonly IConsoleUI _consoleUI;
  private readonly ICommandHistory _commandHistory;

  public Console(IContext context, IScriptEngine scriptEngine, IConsoleUI consoleUI, ICommandHistory commandHistory)
  {
    _context = context;
    _scriptEngine = scriptEngine;
    _consoleUI = consoleUI;
    _commandHistory = commandHistory;
  }

  public async Task Run()
  {
    _consoleUI.ShowWelcome();

    while (true)
    {
      var input = _consoleUI.ReadInput();
      if (string.IsNullOrWhiteSpace(input)) continue;
      if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

      _commandHistory.AddCommand(input);
      var result = await _scriptEngine.Execute(input);
      _consoleUI.ShowResult(result);
    }
  }
}
