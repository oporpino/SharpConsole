using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using SharpConsoleCore.Domain.Outbound;
using SharpConsoleCore.Domain.Inbound;
using System.Linq;
using SharpConsoleCore.Application;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace SharpConsoleCore.Infrastructure;

public class ScriptEngine : IScriptEngine
{
  public async Task<object?> Execute(string command)
  {
    var context = ConsoleContext.Get();
    try
    {
      var options = ScriptOptions.Default
        .WithImports("System", "System.Linq", "System.Collections.Generic", "System.Dynamic", "System.Runtime.CompilerServices")
        .AddReferences(typeof(Enumerable).Assembly)
        .AddReferences(typeof(Dictionary<string, object>).Assembly)
        .AddReferences(typeof(ExpandoObject).Assembly)
        .AddReferences(typeof(DynamicAttribute).Assembly)
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
