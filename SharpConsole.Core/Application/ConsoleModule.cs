using Microsoft.Extensions.DependencyInjection;
using SharpConsoleCore.Domain;
using SharpConsoleCore.Domain.Entities;
using SharpConsoleCore.Domain.Inbound;
using SharpConsoleCore.Domain.Outbound;
using SharpConsoleCore.Domain.UseCases;
using SharpConsoleCore.Infrastructure;

namespace SharpConsoleCore.Application;

public static class ConsoleModule
{
  public static IServiceCollection AddConsoleModule(this IServiceCollection services)
  {
    // Register domain entities
    services.AddSingleton<IConsoleExecutor, ConsoleExecutor>();

    // Register infrastructure implementations for domain interfaces
    services.AddSingleton<IScriptEngine, ScriptEngine>();
    services.AddSingleton<IConsoleDisplay, ConsoleDisplay>();
    services.AddSingleton<ICommandHistory, CommandHistory>();
    services.AddSingleton<IInputHandler, ConsoleInputHandler>();
    services.AddSingleton<IConsoleManager, ConsoleManager>();
    services.AddSingleton<ILineCleaner, ConsoleLineCleaner>();
    services.AddSingleton<IOutputFormatter, JsonOutputFormatter>();

    // Register use cases
    services.AddSingleton<IUseCase<IConsoleExecutor>, CreateConsoleUsecase>();

    // Register application services
    services.AddSingleton<Console>();

    return services;
  }

  public static void Initialize()
  {
    var services = new ServiceCollection();
    services.AddConsoleModule();
    var serviceProvider = services.BuildServiceProvider();
    DependencyContainer.Initialize(serviceProvider);
  }
}
