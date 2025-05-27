using SharpConsole.Domain.Outbound;
using SharpConsole.Domain;

namespace SharpConsole.Core.Application.Internal;

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

    AplicationContext.Set(context);

    var domainContext = DependencyContainer.Instance.GetService<DomainContext>();
    domainContext.RunConsole();
  }
}
