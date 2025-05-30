namespace SharpConsole.Core.Outbound;

public interface IConsoleManager
{
  string GetPreviousCommand();
  string GetNextCommand();
  void ClearCurrentLine();
  void WriteLine(string text);
  void Write(string text);
  void WriteBackspace();
}
