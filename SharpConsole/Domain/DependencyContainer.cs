using Microsoft.Extensions.DependencyInjection;

namespace SharpConsoleCore.Domain;

public static class DependencyContainer
{
  private static IServiceProvider? _serviceProvider;

  public static void Initialize(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public static T GetService<T>() where T : class
  {
    if (_serviceProvider == null)
      throw new InvalidOperationException("DependencyContainer has not been initialized.");

    return _serviceProvider.GetService<T>() ??
      throw new InvalidOperationException($"Service of type {typeof(T)} not found.");
  }
}
