using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using SharpConsole.Core.Domain.Outbound;
using SharpConsole.Core.Domain.Inbound;
using System.Linq;
using SharpConsole.Core.Application;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace SharpConsole.Core.Infrastructure;

public class ScriptEngine : IScriptEngine
{
  private ScriptState<object>? _scriptState;

  public async Task<object?> Execute(string command)
  {
    var context = ConsoleContext.Get();
    try
    {
      var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
        .ToList();

      var exampleAssemblies = assemblies
        .Where(a => a.GetName().Name?.StartsWith("SharpConsoleExamples") == true)
        .ToList();

      var baseImports = new List<string> {
        "System",
        "System.Linq",
        "System.Collections.Generic",
        "System.Dynamic",
        "System.Runtime.CompilerServices"
      };

      if (assemblies.Any(a => a.GetName().Name?.Contains("EntityFrameworkCore") == true))
      {
        baseImports.Add("Microsoft.EntityFrameworkCore");
      }

      var options = ScriptOptions.Default
        .WithImports(baseImports)
        .AddReferences(assemblies)
        .WithEmitDebugInformation(true);

      if (exampleAssemblies.Any())
      {
        var exampleNamespaces = exampleAssemblies
          .SelectMany(a => a.GetTypes())
          .Select(t => t.Namespace)
          .Where(n => n != null)
          .Distinct()
          .ToList();

        options = options.WithImports(exampleNamespaces);
      }

      _scriptState = _scriptState == null
        ? await CSharpScript.RunAsync(command, options, context)
        : await _scriptState.ContinueWithAsync(command);

      if (_scriptState.ReturnValue == null && command.Trim().StartsWith("var "))
      {
        var variableName = command.Split('=')[0].Trim().Split(' ')[1];
        var globals = _scriptState.Variables.FirstOrDefault(v => v.Name == variableName);
        return globals?.Value;
      }

      if (_scriptState.ReturnValue is Task task)
      {
        await task;
        var result = task.GetType().GetProperty("Result")?.GetValue(task);
        return result ?? "Done";
      }

      return _scriptState.ReturnValue ?? "Done";
    }
    catch (Exception ex)
    {
      throw new Exception($"Error executing script: {ex.Message}");
    }
  }
}
