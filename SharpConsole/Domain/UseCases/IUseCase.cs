namespace SharpConsole.Domain.UseCases;

public interface IUseCase<T>
{
  T Execute();
}
