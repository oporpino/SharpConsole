using System.Threading.Tasks;

namespace SharpConsole.Core.Outbound;

public interface IScriptEngine
{
  Task<object?> Execute(string command);
}
