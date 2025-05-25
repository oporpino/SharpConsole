using Microsoft.Extensions.DependencyInjection;
using SharpConsoleCore.Domain.Inbound;
using SharpConsoleCore.Domain.UseCases;

namespace SharpConsoleCore.Application;

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
