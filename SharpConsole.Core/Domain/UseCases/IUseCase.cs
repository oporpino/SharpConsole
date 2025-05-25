namespace SharpConsole.Core.Domain.UseCases;

public interface IUseCase<T>
{
  T Execute();
}
