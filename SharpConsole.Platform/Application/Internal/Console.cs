using SharpConsole.Core.Outbound;
using SharpConsole.Core;

namespace SharpConsole.Platform.Application.Internal;

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
