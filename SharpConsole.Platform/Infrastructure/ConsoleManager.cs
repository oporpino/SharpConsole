using SharpConsole.Core.Outbound;

namespace SharpConsole.Platform.Infrastructure;

public class ConsoleManager : IConsoleManager
{
  private readonly ICommandHistory _commandHistory;

  public ConsoleManager(ICommandHistory commandHistory)
  {
    _commandHistory = commandHistory;
  }

  public string GetPreviousCommand()
  {
    return _commandHistory.NavigateUp();
  }

  public string GetNextCommand()
  {
    return _commandHistory.NavigateDown();
  }

  public void ClearCurrentLine()
  {
    var currentLine = System.Console.CursorTop;
    System.Console.SetCursorPosition(0, currentLine);
    System.Console.Write(new string(' ', System.Console.WindowWidth));
    System.Console.SetCursorPosition(0, currentLine);
  }

  public void WriteLine(string text)
  {
    System.Console.WriteLine(text);
  }

  public void Write(string text)
  {
    System.Console.Write(text);
  }

  public void WriteBackspace()
  {
    System.Console.Write("\b \b");
  }
}
