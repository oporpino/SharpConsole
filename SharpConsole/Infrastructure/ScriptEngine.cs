using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.Inbound;
using System.Linq;
using SharpConsole.Application;
using System.Collections.Generic;

namespace SharpConsole.Infrastructure;

public class ScriptEngine : IScriptEngine
{
  public async Task<object?> Execute(string command)
  {
    var context = ConsoleContext.Get();
    try
    {
      var options = ScriptOptions.Default
        .WithImports("System", "System.Linq", "System.Collections.Generic")
        .AddReferences(typeof(Enumerable).Assembly)
        .AddReferences(typeof(Dictionary<string, object>).Assembly)
        .WithEmitDebugInformation(true);

      var scriptState = await CSharpScript.RunAsync(command, options, context);
      return scriptState.ReturnValue;
    }
    catch (Exception ex)
    {
      throw new Exception($"Error executing script: {ex.Message}");
    }
  }
}
