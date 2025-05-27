namespace SharpConsole.Core.Outbound;

public interface IOutputFormatter
{
  string Format(object? value);
}
