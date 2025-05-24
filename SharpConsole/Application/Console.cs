using Microsoft.Extensions.DependencyInjection;
using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.UseCases;

namespace SharpConsole.Application;

internal class Console
{
  public static async Task Start(IContext context)
  {
    ConsoleContext.SetContext(context);

    var console = CreateConsoleUsecase.Call();
    await console.Start();
  }
}
