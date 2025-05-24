using SharpConsole;
using SharpConsole.Core;
using SharpConsole.Scripting;
using SharpConsole.UI;

namespace SharpConsole.Example;

public class CustomContext : IContext
{
  private readonly List<string> _names = new() { "Alice", "Bob", "Charlie" };
  private readonly List<int> _scores = new() { 85, 92, 78 };

  public object GetContext()
  {
    return new ScriptContext(_names, _scores);
  }
}

public class ScriptContext
{
  public List<string> Names { get; }
  public List<int> Scores { get; }

  public ScriptContext(List<string> names, List<int> scores)
  {
    Names = names;
    Scores = scores;
  }
}

public class Program
{
  public static async Task Main()
  {
    var context = new CustomContext();
    var scriptEngine = new ScriptEngine(context);
    var consoleUI = new ConsoleUI();

    var application = new SharpConsole.Application(context, scriptEngine, consoleUI);

    await application.Run();
  }
}
