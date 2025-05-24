using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.Inbound;

namespace SharpConsole.Infrastructure;

public class ScriptEngine : IScriptEngine
{
  private readonly IContext _context;

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

      var scriptState = await CSharpScript.RunAsync(code, options, _context.GetContext());
      return scriptState.ReturnValue;
    }
    catch (Exception ex)
    {
      throw new Exception($"Error executing script: {ex.Message}");
    }
  }
}
