using SharpConsole.Core.Domain.Outbound;

namespace SharpConsole.Core.Infrastructure;

public class ConsoleLineCleaner : ILineCleaner
{
  private const string PROMPT = "> ";

  public void Clear()
  {
    var currentLine = System.Console.CursorTop;
    System.Console.SetCursorPosition(PROMPT.Length, currentLine);
    System.Console.Write(new string(' ', System.Console.WindowWidth - PROMPT.Length));
    System.Console.SetCursorPosition(PROMPT.Length, currentLine);
  }
}
