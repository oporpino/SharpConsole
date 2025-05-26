using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.Inbound;
using System.Linq;
using SharpConsole.Core.Application;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace SharpConsole.Core.Infrastructure;

public class ScriptEngine : IScriptEngine
{
  private ScriptState<object>? _scriptState;
  private ScriptOptions _scriptOptions;

  public ScriptEngine()
  {
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
      .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
      .ToList();

    var baseImports = new List<string> {
      "System",
      "System.Linq",
      "System.Collections.Generic",
      "System.Dynamic",
      "System.Runtime.CompilerServices",
      "System.Threading.Tasks"
    };

    _scriptOptions = ScriptOptions.Default
      .WithImports(baseImports)
      .AddReferences(assemblies)
      .WithEmitDebugInformation(true)
      .WithOptimizationLevel(OptimizationLevel.Debug);
  }

  public async Task<object?> Execute(string command)
  {
    var context = ConsoleContext.Get();
    try
    {
      var script = _scriptState == null
        ? CSharpScript.Create(command, _scriptOptions, context.GetType())
        : _scriptState.Script.ContinueWith(command);

      _scriptState = await script.RunAsync(context);

      // Update context with new variables
      if (_scriptState != null)
      {
        foreach (var variable in _scriptState.Variables)
        {
          var property = context.GetType().GetProperty(variable.Name);
          if (property != null)
          {
            property.SetValue(context, variable.Value);
          }
        }
      }

      if (_scriptState.ReturnValue == null && command.Trim().StartsWith("var "))
      {
        var variableName = command.Split('=')[0].Trim().Split(' ')[1];
        var scriptVariable = _scriptState.Variables.FirstOrDefault(v => v.Name == variableName);
        return scriptVariable?.Value;
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

public class ScriptGlobals : DynamicObject
{
  private readonly Dictionary<string, object> _variables;

  public ScriptGlobals(dynamic context)
  {
    _variables = new Dictionary<string, object>();

    // Copy all properties from context
    foreach (var property in context.GetType().GetProperties())
    {
      var value = property.GetValue(context);
      _variables[property.Name] = value;
    }
  }

  public void SetVariable(string name, object value)
  {
    _variables[name] = value;
  }

  public override bool TryGetMember(GetMemberBinder binder, out object result)
  {
    return _variables.TryGetValue(binder.Name, out result);
  }

  public override bool TrySetMember(SetMemberBinder binder, object value)
  {
    _variables[binder.Name] = value;
    return true;
  }
}
