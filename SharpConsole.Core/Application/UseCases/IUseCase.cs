namespace SharpConsole.Core.Application.UseCases;

public interface IUseCase<T>
{
  T Execute();
}
