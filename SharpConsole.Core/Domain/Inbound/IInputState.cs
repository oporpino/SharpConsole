namespace SharpConsole.Core.Domain.Inbound;

public interface IInputState
{
  string Text { get; }
  int Position { get; }
}
