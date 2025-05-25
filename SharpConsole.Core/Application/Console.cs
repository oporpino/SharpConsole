using Microsoft.Extensions.DependencyInjection;
using SharpConsole.Core.Domain.Inbound;
using SharpConsole.Core.Domain.UseCases;

namespace SharpConsole.Core.Application;

internal class Console
{
  private static bool _isInitialized;

  internal static void Start(dynamic context)
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
