namespace SharpConsole.Scripting;

public interface IScriptEngine
{
  Task<object?> Execute(string code);
}
