namespace SharpConsole.Domain.Outbound;

public interface IScriptEngine
{
  Task<object?> Execute(string command);
}
