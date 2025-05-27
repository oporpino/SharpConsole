using Microsoft.Extensions.DependencyInjection;

namespace SharpConsole.Platform.Entrypoint.Internal;

internal sealed class DependencyContainer
{
  private static readonly DependencyContainer _instance = new();
  private IServiceProvider? _serviceProvider;

  private DependencyContainer() { }

  internal static DependencyContainer Instance => _instance;

  internal void Initialize(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  internal T GetService<T>() where T : class
  {
    if (_serviceProvider == null)
      throw new InvalidOperationException("DependencyContainer has not been initialized.");

    return _serviceProvider.GetService<T>() ??
      throw new InvalidOperationException($"Service of type {typeof(T)} not found.");
  }
}
