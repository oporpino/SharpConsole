using SharpConsole.Core;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace SharpConsole.Scripting;

public class ScriptEngine : IScriptEngine
{
  private readonly ScriptOptions _options;
  private readonly IContext _context;

  public ScriptEngine(IContext context)
  {
    _context = context;
    _options = ScriptOptions.Default
        .AddReferences(typeof(System.Console).Assembly)
        .AddReferences(typeof(System.Linq.Enumerable).Assembly)
        .AddImports("System")
        .AddImports("System.Linq");
  }

  public async Task<object?> Execute(string code)
  {
    try
    {
      var context = _context.GetContext();
      var script = CSharpScript.Create(code, _options, context.GetType());
      var result = await script.RunAsync(context);
      return result.ReturnValue;
    }
    catch (Exception ex)
    {
      return ex.Message;
    }
  }
}
