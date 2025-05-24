using SharpConsole.UI;
using SharpConsole.Core;

namespace SharpConsole.UI;

public class ConsoleUI : IConsoleUI
{
  private const string PROMPT = "> ";
  private const string WELCOME_MESSAGE = "Welcome to SharpConsole! Type your C# code and press Enter to execute.";
  private const string ERROR_PREFIX = "Error: ";
  private readonly IOutputFormatter _formatter;
  private readonly ICommandHistory _commandHistory;

  public ConsoleUI(IOutputFormatter formatter, ICommandHistory commandHistory)
  {
    _formatter = formatter;
    _commandHistory = commandHistory;
  }

  public string ReadInput()
  {
    System.Console.Write(PROMPT);
    var input = string.Empty;
    var currentPosition = 0;

    while (true)
    {
      var key = System.Console.ReadKey(true);

      if (key.Key == ConsoleKey.Enter)
      {
        System.Console.WriteLine();
        return input;
      }

      if (key.Key == ConsoleKey.UpArrow)
      {
        var historyCommand = _commandHistory.NavigateUp();
        if (!string.IsNullOrEmpty(historyCommand))
        {
          ClearCurrentLine();
          input = historyCommand;
          currentPosition = input.Length;
          System.Console.Write(input);
        }
        continue;
      }

      if (key.Key == ConsoleKey.DownArrow)
      {
        var historyCommand = _commandHistory.NavigateDown();
        ClearCurrentLine();
        input = historyCommand;
        currentPosition = input.Length;
        System.Console.Write(input);
        continue;
      }

      if (key.Key == ConsoleKey.Backspace && currentPosition > 0)
      {
        input = input.Remove(currentPosition - 1, 1);
        currentPosition--;
        System.Console.Write("\b \b");
        continue;
      }

      if (!char.IsControl(key.KeyChar))
      {
        input = input.Insert(currentPosition, key.KeyChar.ToString());
        currentPosition++;
        System.Console.Write(key.KeyChar);
      }
    }
  }

  private void ClearCurrentLine()
  {
    var currentLine = System.Console.CursorTop;
    System.Console.SetCursorPosition(PROMPT.Length, currentLine);
    System.Console.Write(new string(' ', System.Console.WindowWidth - PROMPT.Length));
    System.Console.SetCursorPosition(PROMPT.Length, currentLine);
  }

  public void ShowResult(object? result)
  {
    System.Console.WriteLine(_formatter.Format(result));
  }

  public void ShowWelcome()
  {
    System.Console.WriteLine(WELCOME_MESSAGE);
    System.Console.WriteLine();
  }

  public void ShowError(string message)
  {
    System.Console.ForegroundColor = ConsoleColor.Red;
    System.Console.WriteLine($"{ERROR_PREFIX}{message}");
    System.Console.ResetColor();
  }
}
