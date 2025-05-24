using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.Inbound;
using System.Linq;
using SharpConsole.Application;

namespace SharpConsole.Infrastructure;

public class ScriptEngine : IScriptEngine
{
  public async Task<object?> Execute(string command)
  {
    var context = ConsoleContext.GetContext();
    try
    {
      var options = ScriptOptions.Default
        .WithImports("System", "System.Linq")
        .AddReferences(typeof(Enumerable).Assembly)
        .WithEmitDebugInformation(true);

      var scriptState = await CSharpScript.RunAsync(command, options, context.GetContext());
      return scriptState.ReturnValue;
    }
    catch (Exception ex)
    {
      throw new Exception($"Error executing script: {ex.Message}");
    }
  }
}
