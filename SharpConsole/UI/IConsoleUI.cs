namespace SharpConsole.UI;

public interface IConsoleUI
{
  string ReadInput();
  void ShowResult(object? result);
  void ShowWelcome();
  void ShowError(string message);
}
