using SharpConsole.Core;
using SharpConsole.Scripting;
using SharpConsole.UI;

namespace SharpConsole;

public class Application
{
  private readonly IContext _context;
  private readonly IScriptEngine _scriptEngine;
  private readonly IConsoleUI _consoleUI;

  public Application(IContext context, IScriptEngine scriptEngine, IConsoleUI consoleUI)
  {
    _context = context;
    _scriptEngine = scriptEngine;
    _consoleUI = consoleUI;
  }

  public async Task Run()
  {
    _consoleUI.ShowWelcome();

    while (true)
    {
      var input = _consoleUI.ReadInput();
      if (string.IsNullOrWhiteSpace(input)) continue;

      var result = await _scriptEngine.Execute(input);
      _consoleUI.ShowResult(result);
    }
  }
}
