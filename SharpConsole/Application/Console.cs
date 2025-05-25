using Microsoft.Extensions.DependencyInjection;
using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.UseCases;

namespace SharpConsole.Application;

internal class Console
{
  private static bool _isInitialized;

  public static void Start(dynamic context)
  {
    if (!_isInitialized)
    {
      ConsoleModule.Initialize();
      _isInitialized = true;
    }

    ConsoleContext.Set(context);

    var executor = CreateConsoleUsecase.Call();
    executor.Execute();
  }
}
