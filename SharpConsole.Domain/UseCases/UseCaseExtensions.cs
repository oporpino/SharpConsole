namespace SharpConsole.Domain.UseCases;

public static class UseCaseExtensions
{
  public static void Call<T>(this T _, params object[] parameters) where T : UseCase<T>
  {
    UseCase<T>.Call(parameters);
  }
}
