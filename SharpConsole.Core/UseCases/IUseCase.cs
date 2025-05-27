namespace SharpConsole.Core.UseCases;

public interface IUseCase<T>
{
  T Execute();
}
