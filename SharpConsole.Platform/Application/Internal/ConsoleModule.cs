using Microsoft.Extensions.DependencyInjection;
using SharpConsole.Domain;
using SharpConsole.Domain.Entities;
using SharpConsole.Domain.Inbound;
using SharpConsole.Domain.Outbound;
using SharpConsole.Domain.UseCases;
using SharpConsole.Platform.Infrastructure;
using SharpConsole.Platform.Application.Internal;
using Console = SharpConsole.Platform.Application.Internal.Console;

namespace SharpConsole.Platform.Application;

internal static class ConsoleModule
{
  internal static IServiceCollection Configure(this IServiceCollection services)
  {
    // Register domain entities
    services.AddSingleton<IConsoleExecutor, ConsoleExecutor>();
    services.AddSingleton<DomainContext>();

    // Register infrastructure implementations for domain interfaces
    services.AddSingleton<IScriptEngine, ScriptEngine>();
    services.AddSingleton<IConsoleDisplay, ConsoleDisplay>();
    services.AddSingleton<ICommandHistory, CommandHistory>();
    services.AddSingleton<IAutoComplete, AutoComplete>();
    services.AddSingleton<IInputHandler, ConsoleInputHandler>();
    services.AddSingleton<IConsoleManager, ConsoleManager>();
    services.AddSingleton<ILineCleaner, ConsoleLineCleaner>();
    services.AddSingleton<IOutputFormatter, JsonOutputFormatter>();

    // Register application services
    services.AddSingleton<Console>();

    return services;
  }

  internal static void Initialize()
  {
    var services = new ServiceCollection();
    services.Configure();

    var serviceProvider = services.BuildServiceProvider();
    DependencyContainer.Instance.Initialize(serviceProvider);
  }
}
