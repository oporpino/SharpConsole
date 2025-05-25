namespace SharpConsole.Core.Domain.Outbound;

public interface IOutputFormatter
{
  string Format(object? value);
}
