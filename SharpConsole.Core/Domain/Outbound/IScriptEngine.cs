namespace SharpConsole.Core.Domain.Outbound;

public interface IScriptEngine
{
  Task<object?> Execute(string code);
}
