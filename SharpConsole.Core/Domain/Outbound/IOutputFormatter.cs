namespace SharpConsoleCore.Domain.Outbound;

public interface IOutputFormatter
{
  string Format(object? value);
}
