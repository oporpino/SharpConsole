using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using SharpConsole.Core.Domain.Outbound;
using SharpConsole.Core.Domain.Inbound;
using System.Linq;
using SharpConsole.Core.Application;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace SharpConsole.Core.Infrastructure;

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
