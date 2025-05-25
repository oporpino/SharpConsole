namespace SharpConsoleCore.Domain.Inbound;

public interface IInputState
{
  string Text { get; }
  int Position { get; }
}
