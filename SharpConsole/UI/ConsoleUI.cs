using SharpConsole.UI;

namespace SharpConsole.UI;

public class ConsoleUI : IConsoleUI
{
  private const string PROMPT = "> ";
  private const string WELCOME_MESSAGE = "Welcome to SharpConsole! Type your C# code and press Enter to execute.";
  private const string ERROR_PREFIX = "Error: ";
  private readonly IOutputFormatter _formatter;

  public ConsoleUI(IOutputFormatter formatter)
  {
    _formatter = formatter;
  }

  public string ReadInput()
  {
    System.Console.Write(PROMPT);
    return System.Console.ReadLine() ?? string.Empty;
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
