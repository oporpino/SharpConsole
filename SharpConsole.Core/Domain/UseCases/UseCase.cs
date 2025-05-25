using Microsoft.Extensions.DependencyInjection;
using SharpConsole.Core.Domain.Inbound;

namespace SharpConsole.Core.Domain.UseCases;

public abstract class UseCase<T> : IUseCase<T>
{
  public abstract T Execute();

  public static T Call()
  {
    var useCase = DependencyContainer.GetService<IUseCase<T>>();
    return useCase.Execute();
  }
}
