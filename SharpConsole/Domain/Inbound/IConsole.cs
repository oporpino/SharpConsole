namespace SharpConsole.Domain.Inbound;

public interface IConsole
{
  Task Start();
  Task ExecuteCommand(string command);
  Task ShowResult(object? result);
  Task ShowError(string message);
  Task ShowWelcome();
}
