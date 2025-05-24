namespace SharpConsole.Domain.Outbound;

public interface IConsoleDisplay : IDisposable
{
  string ReadInput();
  void WriteLine(string message);
  void ShowResult(object? result);
  void ShowError(string message);
  void ShowWelcome();
}
