using SharpConsole.Domain.Entities;
using SharpConsole.Domain.Outbound;

namespace SharpConsole.Infrastructure;

public class ConsoleUI : IConsoleUI
{
  private readonly IOutputFormatter _formatter;
  private readonly ICommandHistory _commandHistory;
  private readonly IInputHandler _inputHandler;
  private const string PROMPT = "> ";
  private readonly CancellationTokenSource _cancellationTokenSource;

  public ConsoleUI(IOutputFormatter formatter, ICommandHistory commandHistory, IInputHandler inputHandler)
  {
    _formatter = formatter;
    _commandHistory = commandHistory;
    _inputHandler = inputHandler;
    _cancellationTokenSource = new CancellationTokenSource();
  }

  public string ReadInput()
  {
    var input = string.Empty;
    var currentPosition = 0;
    System.Console.Write(PROMPT);

    while (!_cancellationTokenSource.Token.IsCancellationRequested)
    {
      if (!System.Console.KeyAvailable)
      {
        Thread.Sleep(100);
        continue;
      }

      var key = System.Console.ReadKey(true);
      var consoleInput = new ConsoleInput(input, currentPosition, key);
      var result = _inputHandler.Handle(consoleInput);

      if (result.IsComplete)
      {
        System.Console.WriteLine();
        _commandHistory.AddCommand(result.Input);
        return result.Input;
      }

      input = result.Input;
      currentPosition = result.Position;
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

  public void ShowResult(object? result)
  {
    var formattedResult = _formatter.Format(result);
    System.Console.WriteLine(formattedResult);
  }

  public void ShowError(string message)
  {
    var previousColor = System.Console.ForegroundColor;
    System.Console.ForegroundColor = ConsoleColor.Red;
    System.Console.WriteLine($"Error: {message}");
    System.Console.ForegroundColor = previousColor;
  }

  public void ShowWelcome()
  {
    System.Console.WriteLine("Welcome to SharpConsole!");
    System.Console.WriteLine("Type 'exit' to quit.");
    System.Console.WriteLine();
  }

  private void ClearCurrentLine()
  {
    var currentLine = System.Console.CursorTop;
    System.Console.SetCursorPosition(PROMPT.Length, currentLine);
    System.Console.Write(new string(' ', System.Console.WindowWidth - PROMPT.Length));
    System.Console.SetCursorPosition(PROMPT.Length, currentLine);
  }
}
