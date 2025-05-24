using SharpConsole.Domain.Outbound;

namespace SharpConsole.Infrastructure;

public class ConsoleManager : IConsoleManager
{
  private readonly ICommandHistory _commandHistory;
  private readonly ILineCleaner _lineCleaner;
  private const string PROMPT = "> ";

  public ConsoleManager(ICommandHistory commandHistory, ILineCleaner lineCleaner)
  {
    _commandHistory = commandHistory;
    _lineCleaner = lineCleaner;
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
    _lineCleaner.Clear();
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
