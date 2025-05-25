namespace SharpConsoleCore.Domain.Outbound;

public interface IScriptEngine
{
  Task<object?> Execute(string code);
}
