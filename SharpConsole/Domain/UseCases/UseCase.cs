using Microsoft.Extensions.DependencyInjection;
using SharpConsoleCore.Domain.Inbound;

namespace SharpConsoleCore.Domain.UseCases;

public abstract class UseCase<T> : IUseCase<T>
{
  public abstract T Execute();

  public static T Call()
  {
    var useCase = DependencyContainer.GetService<IUseCase<T>>();
    return useCase.Execute();
  }
}
