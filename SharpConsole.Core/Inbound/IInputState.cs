namespace SharpConsole.Core.Inbound;

public interface IInputState
{
  string Text { get; }
  int Position { get; }
}
