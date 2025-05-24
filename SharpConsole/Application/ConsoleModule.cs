using Microsoft.Extensions.DependencyInjection;
using SharpConsole.Domain.Outbound;
using SharpConsole.Infrastructure;

namespace SharpConsole.Application;

public static class ConsoleModule
{
  public static IServiceCollection AddConsoleModule(this IServiceCollection services)
  {
    services.AddSingleton<IConsoleManager, ConsoleManager>();
    services.AddSingleton<IInputHandler, ConsoleInputHandler>();
    services.AddSingleton<IConsoleUI, ConsoleUI>();
    services.AddSingleton<ICommandHistory, CommandHistory>();
    services.AddSingleton<ILineCleaner, ConsoleLineCleaner>();

    return services;
  }
}
