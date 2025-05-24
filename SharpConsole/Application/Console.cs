using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.UseCases;
using SharpConsole.Infrastructure;

namespace SharpConsole.Application;

internal class Console
{

  internal static async Task Start(IContext context)
  {
    var scriptEngine = new ScriptEngine(context);
    var formatter = new JsonOutputFormatter();
    var commandHistory = new CommandHistory();
    var consoleManager = new ConsoleManager(commandHistory, new ConsoleLineCleaner());
    var inputHandler = new ConsoleInputHandler(consoleManager);
    var consoleUI = new ConsoleUI(formatter, commandHistory, inputHandler);

    var createConsole = new CreateConsole(scriptEngine, consoleUI, commandHistory);
    var console = createConsole.Execute();

    var runConsole = new RunConsole(console, consoleUI);
    await runConsole.Execute();
  }
}
