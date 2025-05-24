namespace SharpConsole.Domain.Inbound;

public interface IConsole
{
  Task ExecuteCommand(string command);
  Task ShowResult(object? result);
  Task ShowError(string message);
  Task ShowWelcome();
}
