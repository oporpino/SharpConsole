namespace SharpConsoleCore.Domain.UseCases;

public interface IUseCase<T>
{
  T Execute();
}
