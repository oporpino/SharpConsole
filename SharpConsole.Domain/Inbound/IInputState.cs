namespace SharpConsole.Domain.Inbound;

public interface IInputState
{
  string Text { get; }
  int Position { get; }
}
