using SharpConsoleCore.Domain.Entities;
using SharpConsoleCore.Domain.Outbound;

namespace SharpConsoleCore.Infrastructure;

public class ConsoleDisplay : IConsoleDisplay, IDisposable
{
  private readonly IOutputFormatter _formatter;
  private readonly ICommandHistory _commandHistory;
  private readonly IInputHandler _inputHandler;
  private readonly ILineCleaner _lineCleaner;
  private const string PROMPT = "> ";
  private readonly CancellationTokenSource _cancellationTokenSource;

  public ConsoleDisplay(
    IOutputFormatter formatter,
    ICommandHistory commandHistory,
    IInputHandler inputHandler,
    ILineCleaner lineCleaner)
  {
    _formatter = formatter;
    _commandHistory = commandHistory;
    _inputHandler = inputHandler;
    _lineCleaner = lineCleaner;
    _cancellationTokenSource = new CancellationTokenSource();
  }

  public string ReadInput()
  {
    var input = string.Empty;
    var currentPosition = 0;
    Write(PROMPT);

    while (!_cancellationTokenSource.Token.IsCancellationRequested)
    {
      if (!System.Console.KeyAvailable)
      {
        Thread.Sleep(100);
        continue;
      }

      var key = System.Console.ReadKey(true);
      var state = new InputState(input, currentPosition);
      var consoleInput = new ConsoleInput(state, key);
      var result = _inputHandler.Handle(consoleInput);

      if (result.IsComplete)
      {
        WriteLine(string.Empty);
        _commandHistory.AddCommand(result.State.Text);
        return result.State.Text;
      }

      input = result.State.Text;
      currentPosition = result.State.Position;
    }

    return string.Empty;
  }

  public void Dispose()
  {
    _cancellationTokenSource.Cancel();
    _cancellationTokenSource.Dispose();
  }

  public void WriteLine(string message)
  {
    System.Console.WriteLine(message);
  }

  public void Write(string text)
  {
    System.Console.Write(text);
  }

  public void ShowResult(object? result)
  {
    var formattedResult = _formatter.Format(result);
    WriteLine(formattedResult);
  }

  public void ShowError(string message)
  {
    var previousColor = System.Console.ForegroundColor;
    System.Console.ForegroundColor = ConsoleColor.Red;
    WriteLine($"Error: {message}");
    System.Console.ForegroundColor = previousColor;
  }

  public void ShowWelcome()
  {
    WriteLine("Welcome to SharpConsole!");
    WriteLine("Type 'exit' to quit.");
    WriteLine(string.Empty);
  }

  public void ClearCurrentLine()
  {
    _lineCleaner.Clear();
  }

  public void WriteBackspace()
  {
    Write("\b \b");
  }
}
