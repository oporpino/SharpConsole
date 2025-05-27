
using SharpConsole.Domain.Inbound;

namespace SharpConsole.Domain.UseCases;

public abstract class UseCase<T>
{
  public abstract void Execute();

  public static void Call(params object[] parameters)
  {
    var useCase = (UseCase<T>)Activator.CreateInstance(typeof(T), parameters)!;
    useCase.Execute();
  }
}
