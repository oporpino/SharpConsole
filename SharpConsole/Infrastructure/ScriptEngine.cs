using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.Inbound;

namespace SharpConsole.Infrastructure;

public class ScriptEngine : IScriptEngine
{
  private readonly IContext _context;
  private ScriptState<object>? _scriptState;

  public ScriptEngine(IContext context)
  {
    _context = context;
  }

  public async Task<object?> Execute(string code)
  {
    try
    {
      var options = ScriptOptions.Default
        .WithImports("System")
        .WithEmitDebugInformation(true);

      if (_scriptState == null)
      {
        _scriptState = await CSharpScript.RunAsync(code, options, _context.GetContext());
      }
      else
      {
        _scriptState = await _scriptState.ContinueWithAsync(code);
      }

      return _scriptState.ReturnValue;
    }
    catch (Exception ex)
    {
      throw new Exception($"Error executing script: {ex.Message}");
    }
  }
}
