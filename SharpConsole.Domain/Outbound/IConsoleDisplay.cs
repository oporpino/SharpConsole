namespace SharpConsole.Domain.Outbound;

public interface IConsoleDisplay
{
  string ReadInput();
  void WriteLine(string message);
  void Write(string text);
  void ShowResult(object? result);
  void ShowError(string message);
  void ShowWelcome();
  void ClearCurrentLine();
  void WriteBackspace();
}
